# Cookies
In this demo, we wil be playing with the security of our well-known cookies. 

What do Secure, HttpOnly, Lax, Strict or None means in terms of cookies?

## Demo
1. Launch the application from this directory `dotnet run`
2. Open the browser at this URL: [https://localhost:6001](https://localhost:6001)
3. You have two main pages, the `Get Cookies` and the `Set Cookies`
4. Start by going to the Set Cookies page, and then click the `Check the cookies!` link at the bottom.
5. Open the dev tools from your browser and check the flags for these two cookies: Dotnet2023Key and BrowserAndTime
   1. In Chrome, F12, "Application", "Cookies" and select the web page
6. The Dotnet2023Key is unprotected, and represents a bad practice in development, saving a key or password as the value of the cookie.
7. Try the dev-tool's console, and type `document.cookie`
   
You should be seeing the Dotnet2023Key, that can be changed right from the console. Chose any new value, update it (document.cookie='') from the console and refresh the `Get Cookies` page. Hacked!

We were able to to modify its value because of the configuration we set to these cookies. Now we are going to see how to protect them.

## Defense
First, check the configuration we set for the cookies in the 'Program.cs', lines 4-10. We have set **HttpOnly**, which means cookies can not be accessed from JavaScript; we have set **Secure**, which means cookies will only be transmitted over HTTPS channels; and finally, we have set the **SameSite** attribute, which controls if cookies will be sent alongside with requests depending on the site we are targeting.

On the other hand, if you want to check the specific configuration we set for the **unprotected** cookie, have a look at the lines 25-30 of the file `Pages/CookiesSet.cshtml.cs` This is an example of what should NOT be done.

Finally, we recommend you to carefully read the documentation at `https://localhost:6001/`, to fully understand how to protect them. 