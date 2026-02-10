# Chess3D: Multiplayer & AI Strategy Game

**Chess3D** is a fully functional 3D chess implementation developed in Unity. It features a complete ruleset engine, a Minimax-based AI opponent, and real-time multiplayer capability powered by **Photon PUN 2**.

This project demonstrates complex gameplay logic implementation, network state synchronization, and recursive algorithms.

---

## Key Features
- **Complete Chess Logic:** Validates legal moves, En Passant, Castling, Pawn Promotion, Check/Checkmate, and Stalemate detection.
- **Single Player AI:** Implemented a custom **Minimax algorithm** with Alpha-Beta pruning to evaluate board states and optimal moves.
- **Multiplayer:** Real-time online matches using **Photon Unity Networking (PUN 2)** with RPC-based state synchronization.
- **Interactive UI:** Dynamic camera switching, move highlighting, and in-game rule references.

---

## Technical Implementation

### AI Engine (`ChessBotAI.cs`)
The bot utilizes a recursive **Minimax algorithm** to calculate moves:
- **Depth Search:** Configurable search depth to balance performance and difficulty.
- **Evaluation Function:** Calculates board score based on material difference and positional advantages (mobility).
- **Simulation:** Uses a virtual move simulation (`ApplyMoveSimulated` / `UndoSimulated`) to predict outcomes without instantiating objects.

### Networking (`ChessGameManager.cs` & `PhotonLobbyManager.cs`)
- **PUN 2 Integration:** Uses `[PunRPC]` to synchronize moves, turn switching, and game-over states across clients.
- **Lobby System:** Allows players to create and join named rooms for private matches.
- **State Sync:** Ensures complex actions like Castling and Promotion are visually synchronized for both players.

---

## Technical Retrospective (Self-Review 2026)
*This project represents an earlier stage in my development journey. While the gameplay logic is solid and the product is fully playable, the architecture reflects my knowledge at the time.*

**Reflecting on this codebase with my current Middle-level expertise, here is how I would re-engineer this project today:**

1.  **Dependency Injection (DI):**
    * *Current:* Heavy reliance on the Singleton pattern (`ChessGameManager.Instance`, `GameMode.Instance`), leading to tight coupling and testing difficulties.
    * *Future Approach:* I would use **Zenject/Extenject** or **VContainer** to inject services (`INetworkService`, `IBoardService`, `IAIService`) into controllers, removing global state dependencies.

2.  **Architecture & Separation of Concerns:**
    * *Current:* `ChessGameManager` acts as a "God Object," handling UI, game rules, and networking simultaneously.
    * *Future Approach:* I would separate the logic into distinct layers:
        * **Model:** Pure C# classes representing the board state (bitboards or 2D arrays) for faster AI calculation.
        * **View:** Unity Components responsible only for rendering and input.
        * **Controller/Presenter:** Mediation logic connecting Model and View.

3.  **Optimization:**
    * *Current:* The AI uses `FindObjectsByType` during evaluation, which is computationally expensive.
    * *Future Approach:* Cache all piece references in a lightweight data structure to reduce Garbage Collection and CPU usage during the Minimax recursion.

---

## Tech Stack
- **Engine:** Unity 6000.057f1
- **Language:** C#
- **Networking:** Photon PUN 2

## Installation
1. Download the latest Release from the sidebar.
2. Extract the archive.
3. Run `Chess3D.exe`.
