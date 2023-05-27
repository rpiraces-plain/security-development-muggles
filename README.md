# [dotnetconf 2023](https://dotnetconfspain.com/) - Desarrollo seguro para Muggles


This repository contains demos performed in the "Desarrollo seguro para Muggles" talk in the dotnet2023 tech conference.

# Available demos

## azure-goat

**Upstream repository (from [INE labs](https://github.com/ine-labs), [INE web](https://ine.com/)):** https://github.com/ine-labs/AzureGoat/

**LICENSE: [MIT](./azure-goat/LICENSE)**

This repository has been modified to **avoid to deploy extra unneded resources for this demo**, such as VMs, runbooks and others...

### Deploying the infrastructure

**Video explanation:** [Youtube](https://www.youtube.com/watch?v=_FIrMr8hDHY).

**Step by step:**

1. Login into your Azure account in [portal.azure.com](https://portal.azure.com/).

2. Create an empty resource group with the name `azuregoat_app`.

3. Open an Azure Cloud shell in the portal (note: this needs an storage account, that you can create when initializing the shell the first time, which may incur in aditional charges).

4. Clone this repository in your shell: `git clone https://github.com/rpiraces-plain/dotnet2023`

5. Get into the folder `./azure-goat` folder of this repository by executing `cd dotnet2023/azure-goat`.

6. Once in the folder, initialize the terraform state by executing `terraform init`.

7. Deploy the infrastructure by executing `terraform apply`.

8. Wait until the deployment finishes.

9. Once the deployment finishes, you will see the output of the terraform execution, which will contain the main URL of the deployed application.

_**Note:** the deployed resources incur in charges, make sure to stop the function apps to reduce charges._

# Disclaimer

**All of this demos and its contents are vulnerable by default for educational and demo purposes only.**

**Do not install any demo on a production account.**

**This repository contains several bad practices and secrets (for educational purposes only), but not used in production environments.**

**We do not take responsibility for the way in which any one uses these applications.** We have made the purposes of the application clear and it should not be used maliciously. We have given warnings and taken measures to prevent users from installing the repository demos on to production accounts.
