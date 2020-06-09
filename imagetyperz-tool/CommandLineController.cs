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
            else
            {
                if (!d.ContainsKey("-username"))
                {
                    throw new Exception("-username argument is required or use -token");
                }
                if (!d.ContainsKey("-password"))
                {
                    throw new Exception("-password argument is required or use -token");
                }
                this._arguments.set_username(d["-username"]);
                this._arguments.set_password(d["-password"]);

            }

            // mode
            // ----
            if (!d.ContainsKey("-mode")) throw new Exception("-mode argument is required");

            this._arguments.set_mode(d["-mode"]);

            // args
            // ----------------------
            if (d.ContainsKey("-pageurl")) this._arguments.set_page_url((d["-pageurl"]));
            if (d.ContainsKey("-sitekey")) this._arguments.set_site_key((d["-sitekey"]));
            if (d.ContainsKey("-captchaid")) this._arguments.set_captcha_id((d["-captchaid"]));
            if (d.ContainsKey("-output")) this._arguments.set_output_file((d["-output"]));

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
            if (d.ContainsKey("-isphrase")) this._arguments.set_is_phrase();
            if (d.ContainsKey("-alphanumeric")) this._arguments.set_alphanumeric(int.Parse(d["-alphanumeric"]));
            if (d.ContainsKey("-minlength")) this._arguments.set_min_length(int.Parse(d["-minlength"]));
            if (d.ContainsKey("-maxlength")) this._arguments.set_max_length(int.Parse(d["-maxlength"]));

            // geetest
            if (d.ContainsKey("-domain")) this._arguments.set_gt_domain(d["-domain"]);
            if (d.ContainsKey("-challenge")) this._arguments.set_gt_challenge(d["-challenge"]);
            if (d.ContainsKey("-gt")) this._arguments.set_gt_gt(d["-gt"]);
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
            if (!string.IsNullOrWhiteSpace(token)) i = new ImageTypersAPI(token);
            else
            {
                i = new ImageTypersAPI("");
                i.set_user_and_password(a.get_username(), a.get_password());
            }
            switch (a.get_mode())
            {
                case "solve_image":
                    // solve normal captcha
                    // -------------------------
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

                    string resp = i.solve_captcha(captcha_file, id);
                    this.show_output(string.Format("{0}|{1}", i.captcha_id(), resp));
                    break;
                case "submit_recaptcha":
                    string page_url = a.get_page_url();
                    string site_key = a.get_site_key();
                    if (string.IsNullOrWhiteSpace(page_url)) throw new Exception("Invalid recaptcha pageurl");
                    if (string.IsNullOrWhiteSpace(site_key)) throw new Exception("Invalid recaptcha sitekey");

                    Dictionary<string, string> d = new Dictionary<string, string>();
                    d.Add("page_url", page_url);
                    d.Add("sitekey", site_key);

                    // optional
                    if (!string.IsNullOrWhiteSpace(a.get_type())) d.Add("type", a.get_type());
                    if (!string.IsNullOrWhiteSpace(a.get_v3_action())) d.Add("v3_action", a.get_v3_action());
                    if (!string.IsNullOrWhiteSpace(a.get_v3_score())) d.Add("v3_min_score", a.get_v3_score());
                    if (!string.IsNullOrWhiteSpace(a.get_user_agent())) d.Add("user_agent", a.get_user_agent());
                    if (!string.IsNullOrWhiteSpace(a.get_datas())) d.Add("data-s", a.get_datas());
                    if (!string.IsNullOrWhiteSpace(a.get_proxy())) d.Add("proxy", a.get_proxy());
                    string captcha_id = i.submit_recaptcha(d);
                    this.show_output(captcha_id);
                    break;
                case "retrieve_captcha":
                    string recaptcha_id = a.get_captcha_id();
                    if (string.IsNullOrWhiteSpace(recaptcha_id)) throw new Exception("recaptcha id is invalid");
                    string recaptcha_response = i.retrieve_captcha(recaptcha_id);     // get recaptcha response
                    this.show_output(recaptcha_response);       // show response
                    break;
                case "submit_hcaptcha":
                    string page_urlh = a.get_page_url();
                    string site_keyh = a.get_site_key();
                    if (string.IsNullOrWhiteSpace(page_urlh)) throw new Exception("Invalid hCAPTCHA pageurl");
                    if (string.IsNullOrWhiteSpace(site_keyh)) throw new Exception("Invalid hCAPTCHA sitekey");
                    Dictionary<string, string> dh = new Dictionary<string, string>();
                    dh.Add("page_url", string.Format("{0}--hcaptcha", page_urlh));
                    dh.Add("sitekey", site_keyh);
                    string hcaptcha_id_sub = i.submit_recaptcha(dh);
                    this.show_output(hcaptcha_id_sub);
                    break;
                case "submit_capy":
                    string page_urlc = a.get_page_url();
                    string site_keyc = a.get_site_key();
                    if (string.IsNullOrWhiteSpace(page_urlc)) throw new Exception("Invalid capy pageurl");
                    if (string.IsNullOrWhiteSpace(site_keyc)) throw new Exception("Invalid capy sitekey");

                    Dictionary<string, string> dc = new Dictionary<string, string>();
                    dc.Add("page_url", string.Format("{0}--capy", page_urlc));
                    dc.Add("sitekey", site_keyc);
                    string capy_id_sub = i.submit_recaptcha(dc);
                    this.show_output(capy_id_sub);
                    break;
                case "submit_geetest":
                    string gt_domain = a.get_gt_domain();
                    string gt_challenge = a.get_gt_challenge();
                    string gt_gt = a.get_gt_gt();
                    if (string.IsNullOrWhiteSpace(gt_domain)) throw new Exception("Invalid geetest domain");
                    if (string.IsNullOrWhiteSpace(gt_challenge)) throw new Exception("Invalid geetest challenge");
                    if (string.IsNullOrWhiteSpace(gt_gt)) throw new Exception("Invalid geetest gt");

                    Dictionary<string, string> dg = new Dictionary<string, string>();
                    dg.Add("domain", gt_domain);
                    dg.Add("challenge", gt_challenge);
                    dg.Add("gt", gt_gt);

                    // optional
                    if (!string.IsNullOrWhiteSpace(a.get_user_agent())) dg.Add("user_agent", a.get_user_agent());
                    // NEEDS API CHANGES
                    //if (!string.IsNullOrWhiteSpace(a.get_proxy())) dg.Add("proxy", a.get_proxy());
                    string geetest_id_sub = i.submit_geetest(dg);
                    this.show_output(geetest_id_sub);
                    break;

                case "get_balance":
                    string balance = i.account_balance();
                    this.show_output(balance);      // show balance
                    break;
                case "set_captcha_bad":
                    string bad_id = a.get_captcha_id();
                    if (string.IsNullOrWhiteSpace(bad_id)) throw new Exception("captchaid is invalid");
                    string response = i.set_captcha_bad(bad_id);        // set it bad
                    this.show_output(response);     // show response
                    break;
                case "proxy_status":
                    string was_used_id = a.get_captcha_id();
                    if (string.IsNullOrWhiteSpace(was_used_id)) throw new Exception("captchaid is invalid");
                    string rr = i.was_proxy_used(was_used_id);        // set it bad
                    this.show_output(rr);     // show response
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
