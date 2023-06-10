# [dotnetconf 2023](https://dotnetconfspain.com/) - Desarrollo seguro para Muggles


This repository contains demos performed in the "Desarrollo seguro para Muggles" talk in the dotnet2023 tech conference.

# Available actions

In this repository we have enabled several [GitHub Actions](https://docs.github.com/en/actions) to perform different tasks, listed below.
These are mainly focused to perform security analysis of the code, and to detect vulnerabilities in the code and in the dependencies.
The are other actions to simply build some of the demos and provide them out-of-the-box to use them.

## CodeQL Scan

**Purpose: perform static analysis of all the code in this repository.**

Workflow in [./.github/workflows/code_ql.yml](./.github/workflows/code_ql.yml).

With [CodeQL](https://codeql.github.com/) we can perform static analysis of the code, and detect vulnerabilities in the code and in the dependencies.
It's provided by GitHub in a very simple way to use it, and it's free for public repositories to set-up and integrate with the "Security" tab in the repository.
Nevertheless, we can use it in private repositories, and we can also use it in our local machines (via CLI).

In this case the CodeQL action is only meant to be executed on manual demand, and not in every push to the repository (via the `workflow_dispatch` event), for demo purposes.

The action will list all available languages by executing a Python script and try to autobuild all the compiled languages, and then it will execute the CodeQL analysis for all the languages that have been built successfully (if any), along the analysis for the rest of the languages.

We can see the results of this analysis in the "Security" tab of the repository, in the "Code scanning alerts" section (if enabled).

_**Sources used:** [thedave42/multi-lang-monorepo](https://github.com/thedave42/multi-lang-monorepo)._

## Microsoft Defender for DevOps

**Purpose: perform static analysis of all the code in this repository with multiple OSS tools for this (the ones defined by Microsoft Defender for DevOps action). We could see them in Microsoft Defender for Cloud in our Azure Subscription.**

Workflow in [./.github/workflows/microsoft_defender_devops.yml](./.github/workflows/microsoft_defender_devops.yml).

This action only executes the [Microsoft Defender for DevOps action](https://github.com/microsoft/security-devops-action) with the default configuration, which is to execute the following tools:
- [AntiMalware](https://www.microsoft.com/en-us/windows/comprehensive-security): will inspect the code and artifacts for malware.
- [Bandit](https://github.com/PyCQA/bandit): a tool designed to find common security issues in Python code (we are using Python for some demos).
- [BinSkim](https://github.com/Microsoft/binskim): a binary static analysis tool that provides security and correctness results for Windows Portable Executable and *nix ELF binary formats.
- [ESlint](https://github.com/eslint/eslint): a tool for identifying and reporting on patterns found in ECMAScript/JavaScript code.
- [Template Analyzer](https://github.com/Azure/template-analyzer): a tool for identifying security and compliance issues in Azure Resource Manager (ARM) templates and Bicep files.
- [Terrascan](https://github.com/accurics/terrascan): a static code analyzer for Terraform.
- [Trivy](https://github.com/aquasecurity/trivy): all-in-one security scanner to detect vulnerabilities & IaC  misconfigurations, risks and others.

The action also publishes a [SARIF](https://www.oasis-open.org/committees/tc_home.php?wg_abbrev=sarif) file as an artifact, which can be used to integrate with other tools, and inspected for example using [Microsoft SARIF Viewer](https://microsoft.github.io/sarif-web-component/).

This SARIF report will be also integrated and available in the "Security" tab of the repository, in the "Code scanning alerts" section along with others scan results (if enabled). Also, alerts will be present in Microsoft Defender for Cloud in our Azure Subscription.

## Security Scan

**Purpose: perform static analysis of all the code in this repository with multiple OSS tools for this.**

Workflow in [./.github/workflows/security_scan.yml](./.github/workflows/security_scan.yml).

With this action we use the following tools to perform static analysis of the code, and detect vulnerabilities in the code and in the dependencies (and secrets in the code):
- [trivy](https://trivy.dev/): all-in-one security scanner to detect vulnerabilities & IaC  misconfigurations, risks and others.
- [TruffleHog](https://github.com/trufflesecurity/trufflehog): utility to search Git repositories for secrets, digging deep into commit history and branches. This is used to detect secrets in the code.
- [tfsec](https://github.com/aquasecurity/tfsec): Security scanner for Terraform code (used for some demos).
- [bandit](https://github.com/PyCQA/bandit): a tool designed to find common security issues in Python code (we are using Python for some demos).

The action also publishes a [SARIF](https://www.oasis-open.org/committees/tc_home.php?wg_abbrev=sarif) file as an artifact, which can be used to integrate with other tools, and inspected for example using [Microsoft SARIF Viewer](https://microsoft.github.io/sarif-web-component/).

This SARIF report will be also integrated and available in the "Security" tab of the repository, in the "Code scanning alerts" section along with others scan results (if enabled). Also, alerts will be present in Microsoft Defender for Cloud in our Azure Subscription.

## Others:

### Build spy-extension

**Purpose: build the [spy-extension](./spy-extension) and publish it as a GitHub release.**

Workflow in [./.github/workflows/build-spy-extension.yml](./.github/workflows/build-spy-extension.yml).

With the artifact published, we can download the extension and install it in our browser (in development mode, unpacked).
With this extension we can intercept a lot of things that happen in the target browser, and use them (ex. requests, responses, cookies...).

Go to the actions tab, and select the workflow "CI Build for spy-extension", and click on the latest run. In the summary details, you can see in the "Artifacts" section the zip ready to download and use.

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

This repository has been modified to **improve security and some customization for this demo**.
One very important change is the use of [HashIds](https://hashids.org/) for posts ids (instead of sequential numbers), which makes it harder to guess the id of a post and access it.
Posts are kept stored in the DB with the original id, and the HashId is generated on the fly when needed to be provided to the user.

<details>

<summary>**View more about this demo**</summary>  

### Deploying the infrastructure

**Step by step:**

1. Login into your Azure account in [portal.azure.com](https://portal.azure.com/).

2. Create a Azure SQL Server instance (minimum tier recommended).

3. Create two Databases in the SQL Server instance, one named `brokenaccesscontrol` and another named `brokenaccesscontrol_fixed`.

4. Create an App Service Plan (minimum tier recommended).

5. Create two Web Apps in the App Service Plan, try to give them different and coherent names to differentiate them.

6. Clone this repository in your shell: `git clone https://github.com/rpiraces-plain/dotnet2023`

7. Get into the folder `./broken-access-control` folder of this repository by executing `cd dotnet2023/broken-access-control`.

8. Open the main solution file `ProCodeGuide.Samples.BrokenAccessControl.sln` in Visual Studio (Rider or your IDE of preference).

9. Ensure that the `appsettings.json` contains the correct connection string to the SQL Server instance and the correct database name as well as the `Hashids` section for the 'Final' (the fixed one) project.

10. Run the database migrations in both `ProCodeGuide.Samples.BrokenAccessControl` projects by executing `dotnet ef database update` (you may need to install the `dotnet-ef` tool first) in the `./broken-access-control/ProCodeGuide.Samples.BrokenAccessContrrol/Final` and `./broken-access-control/ProCodeGuide.Samples.BrokenAccessContrrol/Initial` folder.

11. Build the application in order to publish it as a package (you can use extensions to directly deploy to your created resources). 

12. Wait until the deployment finishes.

13. You are done! You can access both vulnerable and "security hardened" versions of the web in different URLs and check for the main vulnerability to exploit.

_**Note:** the deployed resources incur in charges, make sure to stop/delete the web apps and SQL server to reduce charges._

### Main vulnerability to exploit

WIP

### Detecting the vulnerability and attempting to stop it before reaeching production

WIP

</details>

## spy-extension

**Upstream repository (from [Matt Frisbie](https://github.com/msfrisbie), [Matt Frisbie web](https://www.mattfriz.com/)):** https://github.com/msfrisbie/spy-extension

**LICENSE: [MIT](./spy-extension/LICENSE)**

This repository has been modified to **upgrade dependencies**.

<details>

<summary>**View more about this demo**</summary>  

**Video explanation:** [Youtube](https://www.youtube.com/watch?v=cIGESSm39n4).

**Related article:** [Substack](https://mattfrisbie.substack.com/p/spy-chrome-extension).

### Building the extension

_**Note:** the extension is build and zipped ready to use by the action available in the repository at [.github/workflows/build-spy-extension.yml](./.github/workflows/build-spy-extension.yml). You can go to the related action and download the artifact from the last run._

**Step by step:**

1. Clone this repository in your shell: `git clone https://github.com/rpiraces-plain/dotnet2023`

2. Get into the folder `./spy-extension` folder of this repository by executing `cd dotnet2023/spy-extension`.

3. Install the dependencies by executing `yarn` (ensure you have [Yarn 1.x](https://classic.yarnpkg.com/en/) installed).

4. Build the extension by executing `yarn build`.

5. You are done! You can [load the extension unpacked](https://developer.chrome.com/docs/extensions/mv3/getstarted/development-basics/#load-unpacked) (will be available at the path `./spy-extension/dist`) in your browser and check for the main vulnerability to exploit.

### Inspecting the extension

Once you have the extension loaded in your browser, you can inspect it by going to the [Extensions page](chrome://extensions):
![spy-extension installed](/assets/spy-extension-installed.png)

Take a look at the details of the extension (you will notice the vast amount of permissions it requires, used to spy on the user):
![spy-extension extension details](/assets/spy-extension-details.png)

Try now to navigate a little bit and open the extension popup (it looks pretty legit):
![spy-extension popup](/assets/spy-extension-popup.png)

Now click on the extension logo and in the "The goods" button to see the data collected by the extension (it will be opened in a new tab):
![spy-extension dashboard](/assets/spy-extension-dashboard.png)

It's pretty impressive all the data it has been collecting... You can click on the buttons to collect more data such as "Cookies", which will be used in the next step.

### Main vulnerability to exploit

Since the extension is spying on the user, it is collecting all the cookies from all the websites the user visits. This means that if the user visits a website with a session cookie, the extension will be able to collect it and send it to the attacker.

We are going to simulate an account takeover at [GitHub.com](https://github.com/) by using the extension to collect the session cookie of the user and then use it to login into the user account in another browser.

**Step by step:**

1. We will be using two browsers, one with the extension installed and another one without it. Open the browser without the extension and go to [GitHub.com](https://github.com/) and log in to your account.

2. Open the browser with the extension and go to [GitHub.com](https://github.com/) and click on the extension logo and in the "The goods" button to see the data collected by the extension (it will be opened in a new tab).

3. Click on the "Cookies" button and search for all cookies related with GitHub. Copy the values of the cookie in the format displayed in your clipboard.

4. Go to the second browser, install the [Cookie-Editor](https://cookie-editor.cgagnier.ca/) extension (this is a legitimate extension that allows you to edit your cookies) and open it.

5. Go to Github.com, click on the "Cookie-Editor" button and then on the "Import" button. Paste the values of the cookie you copied in the previous step and click on the "Import" button (make sure to surrond the copied values by brackets `[` and `]`).

6. Refresh the page and you will be logged in as the user you spied on! Success!


### Detecting the vulnerability and attempting to stop it

You have to be very cautious with these... Do not ever install an extension that you do not trust. If you are not sure about an extension, you can check the source code of the extension (if it is open source) or you can check the permissions it requires (if it requires a lot of permissions, it is kind of suspicious).

You can also check if this extension it's listed in the [Chrome Web Store](https://chrome.google.com/webstore/category/extensions), or the store for your browser (it is not, so it is not a good sign).

There are also some AVs that can detect malicious extensions.

</details>

# Disclaimer

**All of this demos and its contents are vulnerable by default for educational and demo purposes only.**

**Do not install any demo on a production account.**

**This repository contains several bad practices and secrets (for educational purposes only), but not used in production environments.**

**We do not take responsibility for the way in which any one uses these applications.** We have made the purposes of the application clear and it should not be used maliciously. We have given warnings and taken measures to prevent users from installing the repository demos on to production accounts.
