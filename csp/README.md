# Content Security Policy
In this demo, we will try to assess the importance of defining the CSP header, mainly to control which resources are we loading into our web page. By doing so, we can mitigate XSS (Cross Site Scripting) attacks.

## Demo
1. Launch the application from this directory `dotnet run`
2. Open the browser at this URL: [https://localhost:6001/csp](https://localhost:6001/csp)

Ups! We have a problem... Nothing works, but why?

3. Open your developer tools and check the messages of the console. You will see several errors that are preventing the browser from rendering an image, applying inline styles, and evaluating JavaScript code. Now we are going to fix it.
4. First, have a look at the CSP.cshtml and CSP.cshtml.cs files. There you will find the HTML and the code that it is playing with the CSP headers.
   1. In the line 18 of CSP.cshtml.cs we have an strict definition of the CSP header. That's why everything is failling. 
      1. Pay attention to the `report-uri`, it allows as to report to a given endpoint all errors regarding CSP headers. This is usefull if you want to make a transition to a more secure CSP set up.
      2. And also in the server, If you check your terminal in which you launched dotnet, see the reports from the `/collect` endpoint. You should see the errors that the browser is showing you in the console..

   2. Just bellow in the same file, check lines 20-28 to understand the fix that will make our web page work.
   
5. Finally, check the lines 34-37, it is the new way of reporting CSP compliance errors from the browsers. This approach, `Content-Security-Policy-Report-Only` will be detected by major browsers and it is the way to go for the future.



## Defense
In this demo we have seen how to control what is being loaded in our browser. Here we will be giving you three main hints to make your web applications safer.

- Avoid inline styles
- Avoid evaluation of JavaScript code
- And last but not least, be as strict as possible with the URLs you are using to whitelist syles, fonts, and libraries.

**Carefull!** In this demo we have set this piece of code `https://cdn.jsdelivr.net` which whitelists the whole CDN. This is an unwanted behaviour, try to be as specific as possible. By being specific, you will protect your application from loading older versions of a library you are using (deprecated / vulnerable ones).