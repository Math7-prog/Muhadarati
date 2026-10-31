# Open Questions

> **Project:** University Lectures Platform
> **Last updated:** October 31, 2026
> **Purpose:** Collects every point not yet decided, with the party responsible for deciding it. Once a point is resolved, it moves into `project-documentation-ar.md` / `project-documentation-en.md` and is deleted from here.

---

## ✅ Resolved in this update

For traceability only — full details now live in `project-documentation-ar.md` and `project-documentation-en.md`:

- **Registration method:** email + password (Section 3.1), plus an additional verification step via a code sent to the student's university email to unlock AI. The uniqueness constraint on the verified university email solves the multi-account problem, so the separate "fair usage limit" question was dropped.
- **Model training:** cancelled permanently. Full reliance on RAG (Section 7.2).
- **Blocking lecture downloads:** the idea was dropped. Downloads are always allowed on every lecture (Section 5.6).
- **Morning and evening programs:** lectures are shared by default, with an optional tag when they genuinely differ (Section 4.2).
- **Shared subjects:** resolved — one or two subjects are actually shared, and the system supports this via a link table (Section 4.2 and the data model).
- **Deployment strategy:** development and testing on free hosting, then migration to the college's server later (Section 13.2/13.3).
- **The entire Phase 1 database:** the academic structure, Identity, and every remaining table are actually implemented (29 tables), except `LectureChunks` — see `project-status.md` and Sections 14.1/14.1.1/14.1.2 of the specification.

---

## Summary (remaining questions)

| # | Question | Owner | Priority |
|---|---|---|---|
| 1 | College server specs + choice of temporary free hosting | Team + IT department | High |
| 2 | Mail server (for both verification codes and notifications) | IT department | **Very high** — now core to login itself |
| 3 | Final model selection for each AI feature | Team | Medium (initial proposal ready below) — **also determines the vector dimension for the deferred `LectureChunks` table** |
| 4 | Choosing between Judge0 and Piston | Team | Low |
| 5 | Server-side backup (database and lecture files) | IT department | Medium |
| 6 | Project name | Team | Low |

---

## 1. College Server Specs + Temporary Hosting

**Decision already made:** build and test on free hosting now, migrate to the college server once it's ready (documented in the main specification).

**What remains open:**
- **Which specific free hosting** to use for development, supporting: .NET containers, PostgreSQL with the pgvector extension, and Docker (for code execution). Initial suggestions for discussion:
  - **For the app:** Render, Railway, or Fly.io (all support Docker-based deployment).
  - **For the database:** Supabase or Neon (managed PostgreSQL with pgvector on their free tiers).
  - **Caveat:** these free tiers are limited (storage, uptime, connection counts), so verify current real limits before committing, and treat them as suitable for a small pilot audience only — not the full 2,000 students.
- **Final college server specs** (for production later): CPU, RAM, storage, GPU availability, OS (Linux/Windows), Docker support, domain and SSL certificate availability.

---

## 2. Mail Server (now very high priority)

**Why this got more important:** following the registration decision (Section 3.1 of the specification), email is no longer just a notification channel — it is now **essential to activating AI accounts** via the OTP verification code. Any outage here blocks AI sign-in entirely, not just notifications.

**Questions:**
- Does the college already provide an SMTP mail server usable now (even during development on free hosting)?
- If not yet available: we suggest a transactional email service with a free tier (e.g. Resend, Brevo, or Mailjet) for development, to be swapped for the college's server once available — thanks to the "settings via environment variables" design, this requires no code change.
- What is the daily sending limit on either option? (More sensitive now than before, because of verification codes.)

---

## 3. Model Selection per Feature — Initial, Non-Final Proposal

> ⚠️ **Not final.** This is a starting point for discussion, based on recent research (October 2026), not a decision. Free-tier limits from providers change constantly, so verify current numbers in each provider's console before committing.

**Additional note (October 31, 2026):** this decision also fixes the **embedding dimension** for the vector column of the `LectureChunks` table, which is deferred in the database — so that table cannot be created until this question is resolved (every other academic-structure table is already implemented; see `project-status.md`).

**Working assumption:** the team has no GPU right now (settled earlier), so the current focus is on free cloud providers rather than local hosting, while keeping the door open for local hosting later if the college server gets a GPU — which requires no code change thanks to the "swappable model per feature" principle.

| Feature | Primary suggestion | Free access route | Alternative | Note |
|---|---|---|---|---|
| Lecture chat (RAG) and explanation | **Gemini Flash** family (via Google AI Studio) | Free tier, no credit card, large context window suited to long lecture texts | **Qwen3** models (e.g. Qwen3-8B or Qwen3-30B-A3B) via OpenRouter | Gemini is known for solid Arabic support and a large context window |
| Arabic ↔ English translation | Same Gemini Flash model (reused) | — | Qwen3-8B (strong Arabic) via OpenRouter | A general-purpose LLM is sufficient here; no need for a dedicated translation model initially |
| Exam generation and grading | Gemini Flash (structured JSON output) | — | Qwen3-30B-A3B via OpenRouter | Needs strict adherence to a JSON schema — test before adopting |
| Reports and presentations | Gemini Flash (large context window fits long content) | — | — | — |
| Code generation | **Qwen3-Coder** family (smaller variant) | Via OpenRouter (free models) or SiliconFlow (very low pay-as-you-go pricing) | Gemini Flash as a fallback | Specialized coding models outperform general-purpose ones specifically at this task |
| Embedding model for lecture indexing (RAG) | **BAAI/BGE-M3** — self-hosted, CPU-only | Fully free, open weights, supports 100+ languages including Arabic | **IBM Granite Embedding Multilingual R2** (only 97M parameters) — lighter and faster on CPU if BGE-M3 is too slow | The one component worth self-hosting instead of using an API, since it is called on every question and indexes thousands of chunks |

**Backup providers for overflow (when Gemini's daily quota runs out):**
- **Groq:** extremely fast inference, a generous free tier on open models (e.g. Llama 3.3 70B), but no embedding support.
- **OpenRouter:** aggregates several free models (tagged `:free`) behind one OpenAI-compatible interface — a useful extra fallback.

**Suggested next step:** once two or three real candidate models are chosen, run a direct test against a sample of real Arabic lectures before final adoption, per the general principle already in the specification.

---

## 4. Choosing Between Judge0 and Piston
**Current status:** both are open source, free, and self-hosted.

**Needed:** test both on the chosen hosting (Section 1 above) and compare ease of setup, performance, input support, and future multi-file support. Depends on Docker support in the hosting environment.

---

## 5. Server-Side Backup
**Question:** who is responsible for backing up the database and lecture files on the server — the IT department or the project team? And how often?

**Additional note after the temporary-hosting decision:** during the development phase on free hosting, backup is the team's responsibility (most free hosting tiers don't provide it automatically). After migrating to the college server, this question goes back to the IT department.

**Note:** this is separate from the personal-space backup, which happens on the student's own device.

---

## 6. Project Name
**Current status:** earlier suggestions were rejected. The repository is currently named "Muhadarati," but this has not been confirmed as the project's final name.
