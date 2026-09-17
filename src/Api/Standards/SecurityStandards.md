# Security Standards

## Identity and Access

All privileged access requires multi-factor authentication.
The platform must enforce least privilege for all service identities.
Secrets must be stored in managed secret storage and never in source control.

## Logging and Monitoring

Security events must be captured in centralized logs.
Alerting must trigger for failed authentication attempts and unusual access patterns.
