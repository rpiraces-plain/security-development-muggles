# Cross-Site Request Forgery
In this demo, we will see the importance of protecting forms with anti-forgery tokens. 

These unique tokens are randomly generated and sent to the user as a hidden form field. But what is the purpose of this token? Requests payloads are usually predictable, the parameters can be inspected by opening the devtools of any browser and checking the networking tab. Thus, an attacker could take advantage of an open session and craft an specific request that the user will not notice, causing an unwanted action on behalf of the user. This could be accomplished by a phishing attack, clickjacking, and others.

In short, adding anti-forgery tokens makes the request unpredictable, adding a random token that it is generated from the server, and given one time only to the user (for every request).

In this demo, we will se how to work with anti-forgery tokens.

## Demo
1. Launch the application from this directory `dotnet run`
2. Open the browser at this URL: [https://localhost:6001/](https://localhost:6001/)
3. Click in the link "My movie list", we are going to add a new movie
   1. Add any movie you like.
4. Use any of your favorite tools to spin an static web server from the `csrf/wwwroot` folder.
   1. For example, in python: ` python -m http.server 8888`
5. Open your browser at `http://localhost:8888/csrf_form.html`
6. Hacked! Maybe you didn't notice, but the iframe you've just opened has sent a request to the page, and giving that it is CSRF unprotected, now you should see a new movie called `Mr. Robot`.

Why is this code vulnerable? We haven't used the right techniques. There are two main ideas behind the anti-CSRF token: we have to **SEND** the token for every form that we present to the user, and then, upon each new request we have to **VALIDATE** it.

## Defense

Start by checking these two files:
1. The form in `AddToList.cshtml` has the `asp-antiforgery` set to false. In other words, the token is not being added to the form.
2. Check now the `HomeController.cs`, line 28. You will see that we have commented out the `ValidateAntiForgeryToken`.
3. Now set to true `asp-antiforgery` and uncomment the `ValidateAntiForgeryToken`
4. The attack shouldn't work now, we are safer than at the beginning!


But what if we want to protect every controller from our server instead? We can do that by  adding the code we have in the lines 25-29 in the `Program.cs`.