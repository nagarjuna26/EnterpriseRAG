# Messaging Standards

## Integration Patterns

Use asynchronous messaging for decoupled integration boundaries.
All message payloads must be schema-versioned and backward compatible.

## Reliability

Messages must be retried using idempotent processing semantics.
Consumer failures must be surfaced through dead-letter queues and operational dashboards.
