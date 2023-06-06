# [dotnetconf 2023](https://dotnetconfspain.com/) - Desarrollo seguro para Muggles


This repository contains demos performed in the "Desarrollo seguro para Muggles" talk in the dotnet2023 tech conference.

# Available demos

## azure-goat

**Upstream repository (from [INE labs](https://github.com/ine-labs), [INE web](https://ine.com/)):** https://github.com/ine-labs/AzureGoat/

**LICENSE: [MIT](./azure-goat/LICENSE)**

This repository has been modified to **avoid to deploy extra unneded resources for this demo**, such as VMs, runbooks and others...

<details>

<summary>**View more about this demo**</summary>

### Deploying the infrastructure

**Video explanation:** [Youtube](https://www.youtube.com/watch?v=_FIrMr8hDHY).

**Step by step:**

1. Login into your Azure account in [portal.azure.com](https://portal.azure.com/).

2. Create an empty resource group with the name `azuregoat_app`.

3. Open an Azure Cloud shell in the portal (note: this needs an storage account, that you can create when initializing the shell the first time, which may incur in aditional charges).

4. Clone this repository in your shell: `git clone https://github.com/rpiraces-plain/dotnet2023`

5. Get into the folder `./azure-goat` folder of this repository by executing `cd dotnet2023/azure-goat`.

6. Once in the folder, initialize the terraform state by executing `terraform init`.

7. Deploy the infrastructure by executing `terraform apply` (the command will prompt for confirmation, confirm the deploy).

8. Wait until the deployment finishes.

9. Once the deployment finishes, you will see the output of the terraform execution, which will contain the main URL of the deployed application (all deployed resources are in the resource group `azuregoat_app`).

_**Note:** the deployed resources incur in charges, make sure to stop the function apps to reduce charges._

### Main vulnerability to exploit

The application has several vulnerabilities and weeknesses, but the main one that we are going to demonstrate is the following:

[SSRF](https://owasp.org/www-community/attacks/Server_Side_Request_Forgery) in the user portal, when submitting a new post.

When we are logged in the application, we can create a new post, and we can specify the URL of the image to be displayed in the post. The application is not secure enough and its vulnerable to SSRF. We can use this vulnerability to make the application to make a request to an internal resource, and get sensitive information from it (with the URI format `file://`):
![SSRF in azure-goat](/assets/ssrf-azure-goat.png)

**Examples of URIs to use:**
- `file:///etc/passwd`
- `file:///proc/self/environ`
- `file:///home/site/wwwroot/local.settings.config` (this file contains the connection string to the Cosmos database, and the storage account key)

When submitting by clicking the "Upload" button and keeping open the browser "Dev Tools" we can see the request being made to the internal resource, which will return the content of the file in a invalid png file we can download (copying the URL sent in the request in the body property of the JSON object sent in the response):

![Request successfull, SSRF executed](/assets/ssrf-request-successful.png)

When we open the downloaded image in a text editor (such as Notepad++, VSCode or others), we can see the content of the file we requested:

![Leaked credentials](/assets/ssrf-invalid-png-leaked-credentials.png)

From there, we can use the leaked credentials to access the Cosmos database and the storage account, and get more information from there (or modify it, or delete it...).

**Explotation steps:** [Youtube](https://www.youtube.com/watch?v=TVFdorqj2oQ&list=PLcIpBb4raSZGdYHKpqIu5Boc2ziga4oGY&index=2) (specifically the videos named "AzureGoat Exploitation : Server Side Request Forgery Part 1" and "AzureGoat Exploitation : Server Side Request Forgery Part 2").

### Detecting the vulnerability and attempting to stop it before reaeching production

Using static analysis tools, we can detect the vulnerability before deploying the application to production.
In this case we have a [GitHub Action](https://github.com/rpiraces-plain/dotnet2023/actions/workflows/security_scan.yml) which runs the following tools:
- [Trivy](https://aquasecurity.github.io/trivy): has multiple scanners that look for security issues, and targets where it can find those issues.
- [TruffleHog](https://github.com/trufflesecurity/trufflehog): to find and verify credentials in this repo.
- [tfsec](https://aquasecurity.github.io/tfsec): to find security issues in the terraform code.
- [Bandit](https://bandit.readthedocs.io/en/latest/): to find security issues in the python code.

With these tools we can see in the latest execution of the action that there are several issues in the code, and we can see the details on the run log. We also publish the results in "serif" format, which we can download and see in any compatible tool (ex. [Microsoft Sarif Viewer](https://microsoft.github.io/sarif-web-component/)).

We can see the code causing the SSRF with bandit (along many others not shown in the image):
![Bandit results](/assets/bandit-results.png)

We can also see some private keys uploaded to the repository with Trivy:
![Trivy results](/assets/trivy-results.png)

Finally with tfsec, we can see multiple issues as well:
![tfsec results](/assets/tfsec-results.png)

_**Note**: regarding the credentials obtained in the SSRF with `file:///home/site/wwwroot/local.settings.config`, it does not contain secrets, the secrets are put in there in the deployment process and should not be accessible. **Also note that Static Analysis is NOT a "silver bullet".**_

</details>

## broken-access-control

**Upstream repository (from [Pro Code Guide](https://github.com/procodeguide), [Pro Code Guide web](https://procodeguide.com)):** https://github.com/procodeguide/ProCodeGuide.Samples.BrokenAccessControl

<details>

<summary>**View more about this demo**</summary>
WIP  

### Deploying the infrastructure
### Main vulnerability to exploit
### Detecting the vulnerability and attempting to stop it before reaeching production

</details>

# Disclaimer

**All of this demos and its contents are vulnerable by default for educational and demo purposes only.**

**Do not install any demo on a production account.**

**This repository contains several bad practices and secrets (for educational purposes only), but not used in production environments.**

**We do not take responsibility for the way in which any one uses these applications.** We have made the purposes of the application clear and it should not be used maliciously. We have given warnings and taken measures to prevent users from installing the repository demos on to production accounts.
