# Clickjacking
In this demo, you will find a basic clickjacking scenario. This attack, as OWASP defines it: "an attacker uses multiple transparent or opaque layers to trick a user into clicking on a button or link on another page when they were intending to click on the top level page". Here, for demonstration purposes, both layers will be slightly visible. 

To represent this attack we will be having one main application and another one spined from any static web server. Our intention here is to open an html that will be calling with an iframe our main web application. 

## Demo
1. Launch the application from this directory `dotnet run`
2. Open the browser at this URL: [https://localhost:6001/Home/AddToList](https://localhost:6001/Home/AddToList)
3. Use any of your favorite tools to spin an static web server from the `clickjacking/wwwroot` folder.
   1. For example, in python: ` python -m http.server 8888`
4. Open your browser at `http://localhost:8888/clickjacking_iframe.html`

If everything is right, you will see a page that it is offering you a free XBOX, and unfortunately our attack didn't work out. Why?

The key here is that we are setting the "X-Frame-Options" in our web application, and this prevents our app for being loaded. Have a look at the Program.cs.

```
// Line 17
options.SuppressXFrameOptionsHeader = false;
```

If you set this previous line to true, and run again the test, it should work out now. Visit again the same webpage from step 2, and try to click the `hacked` button. If you do it, you will see that you are interacting with the main application, hacked!

## Potential real scenarios
This attack can be used to trick the user into clicking and interacting with real web applications in an unwanted manner. You could potentially perform a delete action, add items to a cart, and so on.

## Defense
We have seen the X-Frame-Options so far, but even though this approach works, there is another one which offers more capabilities, the CSP "frame-ancestors".

In the Program.cs, you have several commented out lines (30-37) that allow us to whitelist an specific domain in which our main web application can be embed. The frame-ancestors header it is interesting because you can define several domains, and it is a well-known and recommended approach.

Try to play around activating and deactivating both the X-Frame-Options, and the CSP frame-ancestors.


