# EnterpriseRAG
Enterprise Standards RAG Assistant

Overview
Enterprise Standards RAG Assistant is a Retrieval-Augmented Generation (RAG) proof-of-concept built using ASP.NET Core. The application enables intelligent retrieval of enterprise knowledge from standards and governance documents, helping engineers quickly access architecture guidelines, security policies, API standards, messaging patterns, and operational runbooks.

The solution demonstrates core RAG concepts including document ingestion, chunking, embeddings abstraction, semantic retrieval, vector similarity search, context grounding, and LLM integration.

Features
Document ingestion for PDF and Markdown files
Content chunking and preprocessing
Embedding abstraction layer
In-memory vector storage
Semantic similarity search
Context-aware retrieval pipeline
Prompt construction for LLM consumption
Modular architecture supporting future OpenAI/Azure OpenAI integration

Architecture
Documents (.pdf, .md)
          ↓
Document Parser
          ↓
Chunking Service
          ↓
Embedding Service
          ↓
In-Memory Vector Store
          ↓
Similarity Search
          ↓
Retriever
          ↓
Prompt Builder
          ↓
LLM Provider
          ↓
Response


Knowledge Base

The current implementation includes enterprise-style documents covering:

Architecture Standards
Security Guidelines
API Governance
Messaging Standards
Production Runbooks
Technology Stack
ASP.NET Core
C#
REST APIs
Retrieval-Augmented Generation (RAG)
Prompt Engineering
Semantic Search
Vector Search
In-Memory Vector Storage
Dependency Injection
Learning Objectives

This project was designed to understand RAG architecture end-to-end while following production-inspired design principles:

Document Ingestion
Chunking Strategies
Embeddings
Semantic Retrieval
Vector Search
Context Grounding
Prompt Construction
LLM Integration Patterns
Future Enhancements
Azure OpenAI / OpenAI integration
PostgreSQL + pgvector support
Azure AI Search integration
Qdrant vector database support
Agentic workflows
Standards compliance analysis
Architecture review automation
AI-powered engineering assistant
