# API Standards

## Service Contracts

All APIs must be versioned through a clear contract model.
Use structured JSON payloads for all success and failure responses.

## Reliability

APIs must be idempotent where possible, especially for writes.
Provide explicit timeout, retry, and circuit breaker policies for downstream dependencies.

## Observability

Each API must emit correlation IDs and structured logs for traceability.
