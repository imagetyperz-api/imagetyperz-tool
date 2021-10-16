using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImageTypers;

namespace imagetyperz_tool
{
    class CommandLineController
    {
        private Arguments _arguments;

        public CommandLineController(string[] args)
        {
            this.init_parse(args);      // init & parse args
        }

        /// <summary>
        /// Init command-line arguments
        /// </summary>
        /// <param name="args"></param>
        private void init_parse(string[] args)
        {
            // init dict with key, value pair
            var d = new Dictionary<string, string>();
            for (int i = 0; i < args.Length; i += 2)
            {
                if (i + 1 == args.Length)
                {
                    break;
                }
                d.Add(args[i].Replace("\"", "").Trim(), args[i + 1].Replace("\"", "").Trim());
            }

            // check our dicts length first
            if (d.Count == 0) throw new Exception("no arguments given. Check README.md file for examples on how to use it");

            // not that we have the dict, check for what we're looking for
            // init the Arguments obj
            // ------------------------------------------------------------
            this._arguments = new Arguments();      // init new obj

            // check what we're looking for
            // ----------------------------
            if (d.ContainsKey("-token")) this._arguments.set_token(d["-token"]);  // we have token

            // mode
            // ----
            if (!d.ContainsKey("-mode")) throw new Exception("-mode argument is required");

            this._arguments.set_mode(d["-mode"]);

            // args
            if (d.ContainsKey("-pageurl")) this._arguments.set_page_url((d["-pageurl"]));
            if (d.ContainsKey("-sitekey")) this._arguments.set_site_key((d["-sitekey"]));
            if (d.ContainsKey("-captchaid")) this._arguments.set_captcha_id((d["-captchaid"]));
            if (d.ContainsKey("-output")) this._arguments.set_output_file((d["-output"]));
            if (d.ContainsKey("-response_only")) this._arguments.set_response_only(true);

            // reCAPTCHA
            if (d.ContainsKey("-type")) this._arguments.set_type(d["-type"]);
            if (d.ContainsKey("-v3minscore")) this._arguments.set_v3_score(d["-v3minscore"]);
            if (d.ContainsKey("-v3action")) this._arguments.set_v3_action(d["-v3action"]);
            if (d.ContainsKey("-datas")) this._arguments.set_datas(d["-datas"]);
            if (d.ContainsKey("-useragent")) this._arguments.set_user_agent(d["-useragent"]);
            if (d.ContainsKey("-proxy")) this._arguments.set_proxy(d["-proxy"]);

            // image
            if (d.ContainsKey("-image")) this._arguments.set_captcha_file((d["-image"]));
            if (d.ContainsKey("-iscase")) this._arguments.set_case_sensitive(true);
            if (d.ContainsKey("-ismath")) this._arguments.set_is_math();
            if (d.ContainsKey("-invisiblehcaptcha")) this._arguments.set_invisible_hcaptcha();
            if (d.ContainsKey("-isphrase")) this._arguments.set_is_phrase();
            if (d.ContainsKey("-alphanumeric")) this._arguments.set_alphanumeric(int.Parse(d["-alphanumeric"]));
            if (d.ContainsKey("-minlength")) this._arguments.set_min_length(int.Parse(d["-minlength"]));
            if (d.ContainsKey("-maxlength")) this._arguments.set_max_length(int.Parse(d["-maxlength"]));

            // geetest
            if (d.ContainsKey("-domain")) this._arguments.set_gt_domain(d["-domain"]);
            if (d.ContainsKey("-challenge")) this._arguments.set_gt_challenge(d["-challenge"]);
            if (d.ContainsKey("-gt")) this._arguments.set_gt_gt(d["-gt"]);

            // funcaptcha
            if (d.ContainsKey("-s_url")) this._arguments.set_s_url(d["-s_url"]);
            if (d.ContainsKey("-data")) this._arguments.set_data(d["-data"]);

            // recaptcha & tiktok
            if (d.ContainsKey("-cookie_input")) this._arguments.set_cookie_input(d["-cookie_input"]);
        }

        /// <summary>
        /// Run method
        /// </summary>
        public void run()
        {
            try
            {
                this._run();
            }
            catch (Exception ex)
            {
                this.save_error(ex.Message);        // save error to error text file
                throw;      // re-throw
            }
        }

        /// <summary>
        /// Private _run method
        /// </summary>
        private void _run()
        {
            var a = this._arguments;        // for easier use local

            ImageTypersAPI i;
            var token = a.get_token();
            if (string.IsNullOrWhiteSpace(token)) throw new Exception("token is missing");
            i = new ImageTypersAPI(token);
            switch (a.get_mode())
            {
                case "get_balance":
                    string balance = i.account_balance();
                    this.show_output(balance);      // show balance
                    break;
                case "submit_image":
                    // solve normal captcha
                    string captcha_file = a.get_captcha_file();
                    if (string.IsNullOrWhiteSpace(captcha_file)) throw new Exception("Invalid captcha file");
                    // optional params
                    Dictionary<string, string> id = new Dictionary<string, string>();

                    // optional
                    if (a.is_case_sensitive()) id.Add("iscase", "true");
                    if (a.get_is_phrase()) id.Add("isphrase", "true");
                    if (a.get_is_math()) id.Add("ismath", "true");
                    if (a.get_alpha_numeric() != -1) id.Add("alphanumeric", a.get_alpha_numeric().ToString());
                    if (a.get_min_length() != -1) id.Add("minlength", a.get_min_length().ToString());
                    if (a.get_max_length() != -1) id.Add("maxlength", a.get_max_length().ToString());

                    string captcha_id = i.submit_image(captcha_file, id);
                    this.show_output(captcha_id);
                    break;
                case "submit_recaptcha":
                    string page_url = a.get_page_url();
                    string site_key = a.get_site_key();
                    if (string.IsNullOrWhiteSpace(page_url)) throw new Exception("Invalid pageurl");
                    if (string.IsNullOrWhiteSpace(site_key)) throw new Exception("Invalid sitekey");

                    Dictionary<string, string> d = new Dictionary<string, string>();
                    d.Add("page_url", page_url);
                    d.Add("sitekey", site_key);

                    // optional
                    if (!string.IsNullOrWhiteSpace(a.get_type())) d.Add("type", a.get_type());
                    if (!string.IsNullOrWhiteSpace(a.get_v3_action())) d.Add("v3_action", a.get_v3_action());
                    if (!string.IsNullOrWhiteSpace(a.get_v3_score())) d.Add("v3_min_score", a.get_v3_score());
                    if (!string.IsNullOrWhiteSpace(a.get_user_agent())) d.Add("user_agent", a.get_user_agent());
                    if (!string.IsNullOrWhiteSpace(a.get_datas())) d.Add("data-s", a.get_datas());
                    if (!string.IsNullOrWhiteSpace(a.get_cookie_input())) d.Add("cookie_input", a.get_cookie_input());
                    if (!string.IsNullOrWhiteSpace(a.get_proxy())) d.Add("proxy", a.get_proxy());
                    if (!string.IsNullOrWhiteSpace(a.get_user_agent())) d.Add("user_agent", a.get_user_agent());
                    string cid = i.submit_recaptcha(d);
                    this.show_output(cid);
                    break;
                case "submit_hcaptcha":
                    string page_urlh = a.get_page_url();
                    string site_keyh = a.get_site_key();
                    if (string.IsNullOrWhiteSpace(page_urlh)) throw new Exception("Invalid pageurl");
                    if (string.IsNullOrWhiteSpace(site_keyh)) throw new Exception("Invalid sitekey");
                    Dictionary<string, string> dh = new Dictionary<string, string>();
                    if (!string.IsNullOrWhiteSpace(a.get_proxy())) dh.Add("proxy", a.get_proxy());
                    if (!string.IsNullOrWhiteSpace(a.get_user_agent())) dh.Add("user_agent", a.get_user_agent());
                    if (a.get_invisible_hcaptcha()) dh.Add("invisible", "1");
                    dh.Add("page_url", page_urlh);
                    dh.Add("sitekey", site_keyh);
                    string hcaptcha_id_sub = i.submit_hcaptcha(dh);
                    this.show_output(hcaptcha_id_sub);
                    break;
                case "submit_capy":
                    string page_urlc = a.get_page_url();
                    string site_keyc = a.get_site_key();
                    if (string.IsNullOrWhiteSpace(page_urlc)) throw new Exception("Invalid pageurl");
                    if (string.IsNullOrWhiteSpace(site_keyc)) throw new Exception("Invalid sitekey");

                    Dictionary<string, string> dc = new Dictionary<string, string>();
                    if (!string.IsNullOrWhiteSpace(a.get_proxy())) dc.Add("proxy", a.get_proxy());
                    if (!string.IsNullOrWhiteSpace(a.get_user_agent())) dc.Add("user_agent", a.get_user_agent());
                    dc.Add("page_url", page_urlc);
                    dc.Add("sitekey", site_keyc);
                    string capy_id_sub = i.submit_capy(dc);
                    this.show_output(capy_id_sub);
                    break;
                case "submit_geetest":
                    string gt_domain = a.get_gt_domain();
                    string gt_challenge = a.get_gt_challenge();
                    string gt_gt = a.get_gt_gt();
                    if (string.IsNullOrWhiteSpace(gt_domain)) throw new Exception("Invalid domain");
                    if (string.IsNullOrWhiteSpace(gt_challenge)) throw new Exception("Invalid challenge");
                    if (string.IsNullOrWhiteSpace(gt_gt)) throw new Exception("Invalid gt");

                    Dictionary<string, string> dg = new Dictionary<string, string>();
                    dg.Add("domain", gt_domain);
                    dg.Add("challenge", gt_challenge);
                    dg.Add("gt", gt_gt);

                    // optional
                    if (!string.IsNullOrWhiteSpace(a.get_proxy())) dg.Add("proxy", a.get_proxy());
                    if (!string.IsNullOrWhiteSpace(a.get_user_agent())) dg.Add("user_agent", a.get_user_agent());
                    string geetest_id_sub = i.submit_geetest(dg);
                    this.show_output(geetest_id_sub);
                    break;
                case "submit_tiktok":
                    string page_urlt = a.get_page_url();
                    string cookie_input = a.get_cookie_input();
                    if (string.IsNullOrWhiteSpace(page_urlt)) throw new Exception("Invalid pageurl");
                    if (string.IsNullOrWhiteSpace(cookie_input)) throw new Exception("Invalid cookie_input");

                    Dictionary<string, string> dcc = new Dictionary<string, string>();
                    if (!string.IsNullOrWhiteSpace(a.get_proxy())) dcc.Add("proxy", a.get_proxy());
                    if (!string.IsNullOrWhiteSpace(a.get_user_agent())) dcc.Add("user_agent", a.get_user_agent());
                    dcc.Add("page_url", page_urlt);
                    dcc.Add("cookie_input", cookie_input);
                    string tiktok_id = i.submit_tiktok(dcc);
                    this.show_output(tiktok_id);
                    break;
                case "submit_funcaptcha":
                    string page_urlfc = a.get_page_url();
                    string site_keyfc = a.get_site_key();
                    if (string.IsNullOrWhiteSpace(page_urlfc)) throw new Exception("Invalid pageurl");
                    if (string.IsNullOrWhiteSpace(site_keyfc)) throw new Exception("Invalid sitekey");
                    Dictionary<string, string> dcf = new Dictionary<string, string>();
                    dcf.Add("page_url", page_urlfc);
                    dcf.Add("sitekey", site_keyfc);

                    // others
                    if (!string.IsNullOrWhiteSpace(a.get_s_url())) dcf.Add("s_url", a.get_s_url());
                    if (!string.IsNullOrWhiteSpace(a.get_data())) dcf.Add("data", a.get_data());
                    if (!string.IsNullOrWhiteSpace(a.get_proxy())) dcf.Add("proxy", a.get_proxy());
                    if (!string.IsNullOrWhiteSpace(a.get_user_agent())) dcf.Add("user_agent", a.get_user_agent());
                    string funcaptcha_id = i.submit_funcaptcha(dcf);
                    this.show_output(funcaptcha_id);
                    break;
                case "retrieve_response":
                    string kid = a.get_captcha_id();
                    if (string.IsNullOrWhiteSpace(kid)) throw new Exception("id is invalid");
                    var recaptcha_response = i.retrieve_response(kid);     // get recaptcha response
                    this.show_output(recaptcha_response);       // show response
                    break;
                case "set_captcha_bad":
                    string bad_id = a.get_captcha_id();
                    if (string.IsNullOrWhiteSpace(bad_id)) throw new Exception("id is invalid");
                    string response = i.set_captcha_bad(bad_id);        // set it bad
                    this.show_output(response);     // show response
                    break;
                default:
                    throw new Exception("invalid mode");
            }
        }

        #region misc
        /// <summary>
        /// Save error to file
        /// </summary>
        /// <param name="error"></param>
        private void save_error(string error)
        {
            this.save_text("error.txt", error);
        }

        /// <summary>
        /// Show output and save to file if given
        /// </summary>
        /// <param name="text"></param>
        private void show_output(string text)
        {
            Console.WriteLine(text);        // print to screen
            // save to file, if set
            if (!string.IsNullOrWhiteSpace(this._arguments.get_output_file())) this.save_text(this._arguments.get_output_file(), text);
        }
        private void show_output(Dictionary<string, string> p)
        {
            string output = "";
            if (p != null)
            {
                if (!this._arguments.get_response_only())
                {
                    output = "{\n";
                    var k = 0;
                    foreach (var key in p.Keys)
                    {
                        string comma = "";
                        if (k < p.Keys.Count - 1) comma = ",";
                        output += String.Format("    \"{0}\": \"{1}\"{2}\n", key, p[key], comma);
                        k += 1;
                    }
                    output += "}";
                }
                else
                {
                    if (p.ContainsKey("Response")) output += p["Response"];
                }
            }
            this.show_output(output);
        }

        /// <summary>
        /// Save text to file
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="text"></param>
        private void save_text(string filename, string text)
        {
            System.IO.File.WriteAllText(filename, text);
        }
        #endregion
    }
}
