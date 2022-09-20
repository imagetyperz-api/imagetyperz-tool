using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace imagetyperz_tool
{
    public class Arguments
    {
        private string _username;
        private string _password;
        private string _mode;

        private string _captcha_file = "";
        private string _output_file = "";
        private string _ref_id = "";

        // recaptcha, capy, hcaptcha and tiktok (tiktok only pageurl)
        private string _page_url = "";
        private string _site_key = "";

        private string _captcha_id = "";
        private string _cookie_input = "";

        private string _proxy = "";
        private string _token = "";
        private string _datas = "";

        // v3
        private string _type = "0";
        private string _v3_action = "";
        private string _v3_score = "";

        private string _user_agent = "";
        private bool _response_only = false;

        // image
        private bool _case_sensitive = false;
        private bool _is_phrase = false;
        private bool _is_math = false;
        private bool _invisible_hcaptcha = false;
        private string _hcaptcha_enterprise = "";
        private int _alphanumeric = -1;
        private int _min_length = -1;
        private int _max_length = -1;

        // geetest
        private string _gt_domain = "";
        private string _gt_challenge = "";
        private string _gt_gt = "";
        private string _gt_geetestid = "";

        // funcaptcha
        private string _s_url = "";
        private string _data = "";

        // task
        private string _template_name = "";
        private string _variables = "";

        public void set_template_name(string s)
        {
            this._template_name = s;
        }

        public void set_variables(string s)
        {
            this._variables = s;
        }

        public string get_template_name()
        {
            return this._template_name;
        }

        public string get_variables()
        {
            return this._variables;
        }


        public void set_s_url(string s)
        {
            this._s_url = s;
        }

        public void set_data(string s)
        {
            this._data = s;
        }

        public string get_s_url()
        {
            return this._s_url;
        }

        public string get_data()
        {
            return this._data;
        }

        public void set_gt_domain(string s)
        {
            this._gt_domain = s;
        }
        public string get_gt_domain()
        {
            return this._gt_domain;
        }
        public void set_gt_challenge(string s)
        {
            this._gt_challenge = s;
        }
        public string get_gt_challenge()
        {
            return this._gt_challenge;
        }
        public void set_gt_gt(string s)
        {
            this._gt_gt = s;
        }
        public string get_gt_gt()
        {
            return this._gt_gt;
        }
        public void set_gt_geetestid(string s)
        {
            this._gt_geetestid = s;
        }
        public string get_gt_geetestid()
        {
            return this._gt_geetestid;
        }
        public void set_user_agent(string user_agent)
        {
            this._user_agent = user_agent;
        }
        public void set_is_phrase()
        {
            this._is_phrase = true;
        }
        public void set_is_math()
        {
            this._is_math = true;
        }
        public void set_invisible_hcaptcha()
        {
            this._invisible_hcaptcha = true;
        }
        public void set_alphanumeric(int n)
        {
            this._alphanumeric = n;
        }
        public void set_min_length(int n)
        {
            this._min_length = n;
        }
        public void set_max_length(int n)
        {
            this._max_length = n;
        }
        public void set_response_only(bool b)
        {
            this._response_only = b;
        }
        public void set_cookie_input (string c)
        {
            this._cookie_input = c;
        }
        public string get_cookie_input()
        {
            return this._cookie_input;
        }
        public bool get_response_only()
        {
            return this._response_only;
        }
        public bool get_is_phrase()
        {
            return this._is_phrase;
        }
        public bool get_is_math()
        {
            return this._is_math;
        }
        public bool get_invisible_hcaptcha()
        {
            return this._invisible_hcaptcha;
        }
        public int get_alpha_numeric()
        {
            return this._alphanumeric;
        }
        public int get_min_length()
        {
            return this._min_length;
        }
        public int get_max_length()
        {
            return this._max_length;
        }
        public string get_user_agent()
        {
            return this._user_agent;
        }
        public void set_v3_score(string score)
        {
            this._v3_score = score;
        }
        public string get_v3_score()
        {
            return this._v3_score;
        }
        public void set_v3_action(string action)
        {
            this._v3_action = action;
        }
        public string get_v3_action()
        {
            return this._v3_action;
        }
        public void set_type(string type)
        {
            this._type = type;
        }
        public string get_type()
        {
            return this._type;
        }
        public string get_token()
        {
            return _token;
        }

        public void set_token(string token)
        {
            this._token = token;
        }

        public string get_username()
        {
            return _username;
        }

        public void set_username(string _username)
        {
            this._username = _username;
        }

        public string get_password()
        {
            return _password;
        }

        public void set_password(string _password)
        {
            this._password = _password;
        }

        public string get_mode()
        {
            return _mode;
        }

        public void set_mode(string _mode)
        {
            this._mode = _mode;
        }

        public string get_captcha_file()
        {
            return _captcha_file;
        }

        public void set_captcha_file(string _captcha_file)
        {
            this._captcha_file = _captcha_file;
        }

        public string get_output_file()
        {
            return _output_file;
        }

        public void set_output_file(string _output_file)
        {
            this._output_file = _output_file;
        }

        public string get_ref_id()
        {
            return _ref_id;
        }

        public void set_ref_id(string _ref_id)
        {
            this._ref_id = _ref_id;
        }

        public string get_page_url()
        {
            return _page_url;
        }

        public void set_page_url(string _page_url)
        {
            this._page_url = _page_url;
        }

        public string get_site_key()
        {
            return _site_key;
        }

        public void set_site_key(string _site_key)
        {
            this._site_key = _site_key;
        }

        public string get_captcha_id()
        {
            return _captcha_id;
        }

        public void set_captcha_id(string _captcha_id)
        {
            this._captcha_id = _captcha_id;
        }

        public bool is_case_sensitive()
        {
            return _case_sensitive;
        }

        public void set_case_sensitive(bool _case_sensitive)
        {
            this._case_sensitive = _case_sensitive;
        }

        public string get_proxy()
        {
            return _proxy;
        }

        public void set_proxy(string _proxy)
        {
            this._proxy = _proxy;
        }

        public string get_datas()
        {
            return _datas;
        }

        public void set_datas(string datas)
        {
            this._datas = datas;
        }

        public string get_hcaptcha_enterprise()
        {
            return _hcaptcha_enterprise;
        }
        public void set_hcaptcha_enterprise(string s)
        {
            this._hcaptcha_enterprise = s;
        }
    }
}
