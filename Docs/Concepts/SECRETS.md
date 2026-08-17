# Secrets: Local vs. Azure Key Vault

Official Microsoft Learn docs for both sides.

## Local secrets (what's implemented in this app now)

- [Safe storage of app secrets in development in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-10.0) — the Secret Manager tool (`dotnet user-secrets`), how it's wired into configuration, and its explicit scope: development-only, unencrypted, per-machine.

## Azure Key Vault (what we'd move to for staging/prod)

- [Azure Key Vault configuration provider in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/security/key-vault-configuration?view=aspnetcore-10.0) — the `AddAzureKeyVault` config provider, `DefaultAzureCredential` for local dev auth vs. managed identity for production.
- [What is Azure Key Vault?](https://learn.microsoft.com/en-us/azure/key-vault/general/basic-concepts) — the service itself: vaults, access policies/RBAC, encryption at rest.
- [About Azure Key Vault secrets](https://learn.microsoft.com/en-us/azure/key-vault/secrets/about-secrets) — secrets specifically (as opposed to keys/certificates), versioning, expiry.

## The core distinction

`user-secrets` is a local file, unencrypted, dev-only, never deployed — literally what we set up. Key Vault is an actual encrypted, access-controlled, network-hosted service meant for real environments, and the same `IConfiguration` pattern (`builder.Configuration.AddAzureKeyVault(...)`) just layers on top the same way `AddUserSecrets` does here — so when you're ready, it's a swap-in, not a redesign.
