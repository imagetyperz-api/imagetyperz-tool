## Imagetyperz Windows command line tool

![screenshot](https://i.imgur.com/JHhopy9.png)

This tool was developed to easily solve captchas through imagetyperz service, from command prompt or through other applications such as WinAutomation, UiPath, UBot ZennoPoster and more.

#### The following captcha types are supported:

- Image captcha (classic)
- reCAPTCHA

  - v2
  - invisible
  - v3 
  - enterprise v2
  - enterprise v3
- GeeTest
- GeeTestV4
- Capy
- hCAPTCHA
- Tiktok
- FunCaptcha
- Turnstile (Cloudflare)
- Task

#### Other supported methods:

- get account balance
- set captcha bad


## Usage

The process of solving captcha is the same for all our supported captcha types:

- submit captcha (gives back an ID)
- retrieve solution using ID

#### Account balance

```imagetyperz-tool.exe -token YOUR_TOKEN -mode get_balance```

In order to write output to file use:

```imagetyperz-tool.exe -token YOUR_TOKEN -mode get_balance -output response.txt```

#### Image captcha

```imagetyperz-tool.exe -token YOUR_TOKEN -mode submit_image -image captcha.jpg```

Returns back an **ID** which will be used to get the response after captcha was solved or check it's progress

#### reCAPTCHA

```imagetyperz-tool.exe -token YOUR_TOKEN -mode submit_recaptcha```

``` -pageurl https://domain.com -sitekey 6ZgGgmzUAAAAALGtLb_FxC0LXm_GwCLyJAffbUCB``` 

Submission of reCAPTCHA supports optional parameters.  Check **All supported arguments** for more details.

#### GeeTest

```imagetyperz-tool.exe -token YOUR_TOKEN -mode submit_geetest```

```-domain https://yourdomain.com -challenge 1a4ac2477c5be6589073cb05ccbd385f -gt 8a361081b2211c344092f2e2fd4f58bf``` 

#### GeeTestV4

```imagetyperz-tool.exe -token YOUR_TOKEN -mode submit_geetest_v4```

```-domain https://example.com -geetestid 647f5ed2ed8acb4be36784e01556bb71``` 

#### Capy

```imagetyperz-tool.exe -token YOUR_TOKEN -mode submit_capy```

``` -pageurl https://domain.com -sitekey 7ZgGgmzUAAAAALGtLb_FxC0LXm_GwCLyJAffbUCA``` 

#### hCAPTCHA

```imagetyperz-tool.exe -token YOUR_TOKEN -mode submit_hcaptcha```

``` -pageurl https://domain.com -sitekey 9ZgGgmzUAAAAALGtLb_FxC0LXm_GwCLyJAffbUCB``` 

#### Tiktok

```
imagetyperz-tool.exe -token YOUR_TOKEN -mode submit_tiktok -pageurl https://tiktok.com -cookie_input s_v_web_id:verify_kd6243o_fd449FX_FDGG_1x8E_8NiQ_fgrg9FEIJ3f;tt_webid:612465623570154;
```

#### FunCaptcha

```
imagetyperz-tool.exe -token YOUR_TOKEN -mode submit_funcaptcha -pageurl https://domain.com -sitekey 11111111-1111-1111-1111-111111111111 -s_url https://api.arkoselabs.com
```

#### Turnstile (Cloudflare)

```
imagetyperz-tool.exe -token YOUR_TOKEN -mode submit_turnstile -pageurl https://domain.com -sitekey 0x4ABBBBAABrfvW5vKbx11FZ
```

#### Task

```imagetyperz-tool.exe -token YOUR_TOKEN -mode submit_task  -pageurl https://imagetyperz.net/automation/login -templatename "Login test page" -variables {\"username\": \"abc\", \"password\": \"paZZW0rd\"}```

#### Task pushVariable

Update a variable value while task is running. Useful when dealing with 2FA authentication.

When template reaches an action that uses a variable which wasn't provided with the submission of the task,
task (while running on worker machine) will wait for variable to be updated through push.

You can use the pushVariables method as many times as you need, even overwriting previously set variables.

```
imagetyperz-tool.exe -token YOUR_TOKEN -mode task_push_variables -captchaid 12345 -variables "{\"twofactor_code\": \"89763\"}"
```



#### Retrieve response

Use this to get the solution after you received a captcha ID.

```imagetyperz-tool.exe -token YOUR_TOKEN -mode retrieve_response -captchaid 4837456```

This should be executed few seconds after the submission, to allow the system to solve the captcha.
If captcha was not solved yet, you'll get an empty response.  In that case, wait 10 more seconds and retry.

```json
{
    "CaptchaId": "178196224",
    "Response": "P0_eyJ0e...O7qSwd_mdrmEjR565LNqQeHkaf4I9DPL7E",
    "Cookie_OutPut": "",
    "Proxy_reason": "",
    "Status": "Solved"
}
```

A JSON object is returned back, which contains the response and some other parameters such as captchaID.

**IMPORTANT**
If you're only interested in the response, `-response_only yes` can be used as arguments.

```imagetyperz-tool.exe -token YOUR_TOKEN -mode retrieve_response -captchaid 4837456 -response_only yes```

#### Set captcha bad

```imagetyperz-tool.exe -token YOUR_TOKEN -mode set_captcha_bad -captchaid 4837456```

### Compiled version

If you don't want to compile from source, you can find the `imagetyperz-tool.exe` compiled inside the `bin` folder

---

#### All supported arguments

- **-mode**
  - `get_balance`
  - `submit_image`
  - `submit_recaptcha`
  - `submit_geetest`
  - `submit_geetest_v4`
  - `submit_capy`
  - `submit_hcaptcha`
  - `submit_tiktok`
  - `submit_funcaptcha`
  - `submit_turnstile`
  - `submit_task`
  - `task_push_variables`
  - `retrieve_response`
  - `set_captcha_bad`
- **-token** (token, for authentication to service API)
- **-output** (output file path, in which to write result. Will print to console (stdout) too, regardless)
- **-captchaid** (used by retrieve methods, set captcha bad and get proxy status)
- **-response_only** (used with `retrieve_response` mode, to return only response instead of JSON object)
- **-pageurl** (`required` when solving reCAPTCHA, Capy, hCAPTCHA, Turnstile)
- **-sitekey** (`required` when solving reCAPTCHA, Capy, hCAPTCHA, Turnstile)
- **-type** (used in solving reCAPTCHA)
  - `1` - v2
  - `2` - invisible
  - `3` - v3
  - `4` - enterprise v2
  - `5` - enterprise v3
- **-v3minscore** (useful when solving v3 recaptcha, when set, has to be a float number, `optional`)
- **-v3action** (once again, in v3 solving, check reCAPTCHA docs to find out how it's used, `optional`)
- **-useragent** (when specified, will be used in solving captchas, `optional`)
- **-datas** (recaptcha data-s value for solving `optional`)
- **-cookie_input** (used in recaptcha and tiktok solving)
- **-proxy** (can be `IP:Port` or `IP:Port:User:Password`, for authentication, `optional`)
- **-image** (used when solving image captcha. It's the path of the image file)
- **-iscase** (case sensitive, `optional` for image captcha only)
- **-isphrase** (tells if captcha is composed of multiple words, `optional` for image captcha only)
- **-ismath** (captcha is mathematical and has to be calculated, `optional` for image captcha only)
- **-invisiblehcaptcha** (used if hcaptcha is invisible, `optional` for hcaptcha only)
- **-hcaptchaenterprise** (extra parameters as stringified JSON, useful in solving hcaptcha enterprise, `optional` for hcaptcha only)
- **-templatename** (used for task captcha, `requred` for task only)
- **-variables** (used for task captcha, `optional` for task only)
- **-alphanumeric**  (`optional` for image captcha only)
  - `1` - digits only
  - `2` - letters only
  - `0`, default (all characters)
- **-minlength** (minimum length of captcha characters, a number, for image captcha, `optional` for image captcha only)
- **-maxlength** (maximum length of captcha characters, a number, for image captcha, `optional` for image captcha only)
- **-domain** (domain used for solving GeeTest captcha, `required`. Also used for solving Turnstile, `optional` in this case)
- **-challenge** (challenge used for solving GeeTest captcha, `required`. Keep in mind, once challenge is used, it gets invalidated and a new one has to be sent for solving)
- **-gt** (gt used for solving GeeTest captcha, `required`)
- **-geetestid** (captchaID used for solving GeetestV4, `required`)
- **-s_url** (used for solving FunCaptcha, `required`)
- **-data** (extra data in JSON format, used for solving FunCaptcha, `optional`)
- **-cdata** (used in solving Turnstile, `optional`)
- **-action** (used in solving Turnstile, `optional`)

---

### License

**GNU GPLv3**

