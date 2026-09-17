# 🏢 Enterprise Standards RAG

> A lightweight **Retrieval-Augmented Generation (RAG)** API built with **.NET 10 Minimal APIs** that answers questions about your enterprise policy, architecture, and security standards.

---

## ✨ Overview

**Enterprise Standards RAG** ingests internal standards documents (Markdown), splits them into searchable chunks, embeds them into an **in-memory vector store**, and serves grounded answers through a simple HTTP API.

- 📚 **Grounded answers** — responses are built from your own standards documents.
- ⚡ **Zero external dependencies** — uses an in-memory vector store and a deterministic embedding service out of the box.
- 🤖 **Pluggable LLM** — optionally connect an OpenAI chat model for natural-language answers.
- 🔌 **Minimal API** — clean, fast endpoints with no controller boilerplate.

---

## 🧩 Key Features

| Feature | Description |
| --- | --- |
| 🔍 **Retrieval** | Finds the most relevant document chunks for a question. |
| 🧠 **Embeddings** | `DeterministicEmbeddingService` for offline/reproducible vectors. |
| 🗂️ **Ingestion** | Auto-seeds all Markdown files from the `Standards/` folder on startup. |
| 💬 **Prompting** | `PromptBuilderService` assembles context-aware prompts. |
| 📦 **Vector Store** | Simple, fast `InMemoryVectorStore`. |

---

## 🏗️ Architecture

```text
Standards/*.md ──▶ ChunkingService ──▶ EmbeddingService ──▶ InMemoryVectorStore
                                                                     │
User Question ──▶ RetrieverService ──▶ PromptBuilderService ──▶ IChatClient ──▶ Answer
```

**Core services** live in `src/Api/Services/`:

- `ChunkingService` — splits documents into chunks.
- `DeterministicEmbeddingService` — generates embeddings.
- `InMemoryVectorStore` — stores and searches vectors.
- `RetrieverService` — retrieves top matches.
- `PromptBuilderService` — builds the LLM prompt.
- `OpenAIChatClient` — talks to the OpenAI chat model.
- `DocumentIngestionService` — loads and indexes documents.

---

## 🚀 Getting Started

### ✅ Prerequisites

- **.NET 10 SDK**

### ▶️ Run the API

```bash
dotnet run --project src/Api
```

The API starts on:

- **HTTP** → `http://localhost:5081`
- **HTTPS** → `https://localhost:7097`

On startup, all Markdown files in `src/Api/Standards/` are **automatically ingested**.

---

## ⚙️ Configuration

Set these values in `appsettings.json`, `appsettings.Development.json`, or environment variables:

| Key | Description | Default |
| --- | --- | --- |
| `OpenAI:ApiKey` | API key for OpenAI chat completions. | _(empty)_ |
| `OpenAI:Model` | Chat model name. | `gpt-4o-mini` |

> ⚠️ **Note:** Without an `OpenAI:ApiKey`, the LLM-backed answers are disabled — retrieval still works.

---

## 📡 API Endpoints

| Method | Route | Description |
| --- | --- | --- |
| `GET` | `/` | Health message confirming the API is running. |
| `GET` | `/health` | Returns `{ "status": "ok" }`. |
| `POST` | `/api/rag/query` | Ask a question and get a grounded answer. |
| `POST` | `/api/rag/chunks` | Manually index a single document chunk. |
| `GET` | `/api/rag/status` | View indexed chunk count and sources. |
| `POST` | `/api/rag/ingest` | Ingest all documents from a directory. |

### 💬 Ask a Question

```bash
curl -X POST http://localhost:5081/api/rag/query \
  -H "Content-Type: application/json" \
  -d '{ "question": "What is required for privileged access?" }'
```

**Response:**

```json
{ "answer": "..." }
```

### 📥 Ingest a Directory

```bash
curl -X POST http://localhost:5081/api/rag/ingest \
  -H "Content-Type: application/json" \
  -d '{ "directoryPath": "src/Api/Standards" }'
```

### 📊 Check Status

```bash
curl http://localhost:5081/api/rag/status
```

---

## 🧪 Testing

```bash
dotnet test
```

Tests live in `tests/EnterpriseStandardsRag.Tests/`.

---

## 📂 Project Structure

```text
EnterpriseStandardsRag/
├── src/
│   └── Api/
│       ├── Endpoints/       # Minimal API endpoint mappings
│       ├── Infrastructure/  # RagPipeline orchestration
│       ├── Models/          # DocumentChunk and DTOs
│       ├── Services/        # Chunking, embeddings, retrieval, chat
│       └── Standards/       # Enterprise standards Markdown docs
└── tests/
    └── EnterpriseStandardsRag.Tests/
```

---

## 📖 Standards Documents

The knowledge base in `src/Api/Standards/` includes:

- 🏛️ **ArchitectureStandards.md**
- 🔒 **SecurityStandards.md**
- 🌐 **ApiStandards.md**
- 📨 **MessagingStandards.md**
- 📕 **ProductionRunbook.md**

---

## 📝 License

Update this section with your project's license (e.g., **MIT**).
