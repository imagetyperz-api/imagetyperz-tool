## Imagetyperz Windows command line tool

![screenshot](https://i.imgur.com/JHhopy9.png)

This tool was developed to easily solve captchas through imagetyperz service, from command prompt or through other applications such as WinAutomation, UiPath, UBot ZennoPoster and more.

#### The following captcha types are supported:

- Image captcha (classic)

- reCAPTCHA

  - v2
  - invisible

  - v3 

- GeeTest

- Capy

- hCAPTCHA

#### Other supported methods:

- get account balance
- get proxy status
- set captcha bad


## Usage

In order to solve reCAPTCHA, GeeTest, Capy or hCAPTCHA the process is the following:

- submit captcha (gives back an ID)
- retrieve solution using ID

Image captcha solving is different. The submission returns both the ID and solution. No need to make a 2nd request to get the solution.

In case of error `error.txt` file will be created, containing the error

#### Account balance

```imagetyperz-tool.exe -token YOUR_TOKEN -mode get_balance```

In order to write output to file use:

```imagetyperz-tool.exe -token YOUR_TOKEN -mode get_balance -output response.txt```

#### Image captcha

Solving image captcha is easy. Just set the image parameter and make sure the mode is set correctly.

```imagetyperz-tool.exe -token YOUR_TOKEN -mode solve_image -image captcha.jpg```

The response contains both the ID and solution: `1233484|polum`

#### reCAPTCHA

```imagetyperz-tool.exe -token YOUR_TOKEN -mode submit_recaptcha```

``` -pageurl https://domain.com -sitekey 6ZgGgmzUAAAAALGtLb_FxC0LXm_GwCLyJAffbUCB``` 

**This returns back an ID. Use the ID to regularly check for completion of captcha using the** `retrieve_captcha` **mode**

Submission of reCAPTCHA supports optional parameters.  Check **All supported arguments** for more details.

#### GeeTest

```imagetyperz-tool.exe -token YOUR_TOKEN -mode submit_geetest```

```-domain https://yourdomain.com -challenge 1a4ac2477c5be6589073cb05ccbd385f -gt 8a361081b2211c344092f2e2fd4f58bf``` 

#### Capy

```imagetyperz-tool.exe -token YOUR_TOKEN -mode submit_capy```

``` -pageurl https://domain.com -sitekey 7ZgGgmzUAAAAALGtLb_FxC0LXm_GwCLyJAffbUCA``` 

#### hCAPTCHA

```imagetyperz-tool.exe -token YOUR_TOKEN -mode submit_hcaptcha```

``` -pageurl https://domain.com -sitekey 9ZgGgmzUAAAAALGtLb_FxC0LXm_GwCLyJAffbUCB``` 

#### Retrieve captcha by id

Use this to get the solution after you received a captcha ID back by submitting: reCAPTCHA, GeeTest, Capy or hCAPTCHA

```imagetyperz-tool.exe -token YOUR_TOKEN -mode retrieve_captcha -captchaid 4837456```

This should be executed few seconds after the submission, to allow the system to solve the captcha.
If captcha was not solved yet, you'll get the following error `NOT_DECODED`.  In that case, wait 5 more seconds and retry.

#### Set captcha bad

```imagetyperz-tool.exe -token YOUR_TOKEN -mode set_captcha_bad -captchaid 4837456```

#### Proxy status

Use this if you want to check the proxy status (if it was used or not), in case you submitted reCAPTCHA with proxy.

```imagetyperz-tool.exe -token YOUR_TOKEN -mode proxy_status -captchaid 4837456```

### Compiled version

If you don't want to compile from source, you can find the `imagetyperz-tool.exe` compiled inside the `bin` folder

---

#### All supported arguments

- **-mode** *
  - `get_balance`
  - `solve_image`
  - `submit_recaptcha`
  - `submit_geetest`
  - `submit_capy`
  - `submit_hcaptcha`
  - `retrieve_captcha`
  - `set_captcha_bad`
  - `proxy_status`
- **-token** ** (token, for authentication to service)
- **-username** ** (instead of using token, use username and password of account to authenticate)
- **-password** ** (password of account)
- **-output** (output file path, in which to write result. Will print to console too)
- **-captchaid** (used by retrieve methods, set captcha bad and get proxy status)
- **-pageurl** (`required` when solving reCAPTCHA, Capy and hCAPTCHA)
- **-sitekey** (`required` when solving reCAPTCHA, Capy and hCAPTCHA)
- **-type** (used in solving reCAPTCHA,  `2` invisible, `3` v3, defaults to `1` regular, `optional`)
- **-v3minscore** (useful when solving v3 recaptcha, when set, has to be a float number, `optional`)
- **-v3action** (once again, in v3 solving, check reCAPTCHA docs to find out how it's used, `optional`)
- **-useragent** (when specified, will be used in solving captchas, `optional`)
- **-datas** (recaptcha data-s value for solving `optional`)
- **-proxy** (can be `IP:Port` or `IP:Port:User:Password`, for authentication. Used in solving reCAPTCHA, `optional`)
- **-image** (used when solving image captcha. It's the path of the image file)
- **-iscase** (case sensitive, `optional` parameter for image captcha)
- **-isphrase** (tells if captcha is composed of multiple words, `optional` parameter for image captcha)
- **-ismath** (captcha is mathematical and has to be calculated, `optional` parameter for image captcha)
- **-alphanumeric** `optional`
  - `1` - digits only
  - `2` - letters only
  - `0`, default (all characters)
- **-minlength** (minimum length of captcha characters, a number, `optional`)
- **-maxlength** (maximum length of captcha characters, a number, `optional`)
- **-domain** (domain used for solving GeeTest captcha, `required`)
- **-challenge** (challenge used for solving GeeTest captcha, `required`. Keep in mind, once challenge is used, it gets invalidated and a new one has to be sent for solving)
- **-gt** (gt used for solving GeeTest captcha, `required`)

---

### License

**GNU GPLv3**

