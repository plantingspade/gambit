# Gambit

> A chess game analyser powered by Stockfish, built in C#

## OVERVIEW

Gambit is a desktop chess analysis tool that syncs your games from Chess.com and lets you understand them at a deeper level. Select any game, step through it move by move, and get explanations of standout moments (blunders, mistakes, and great moves) covering what went wrong, what went right, and why.

I play chess a lot and wanted a way to actually learn from my games rather than just replay them. Gambit is built to do that: sync, analyse, replay, explain.

This is a portfolio project built during my first year studying Computer Science, using C# and a pipeline of open-source tools including Stockfish.

## ARCHITECTURE

The application is designed as a pipeline of loosely coupled components, each with a single responsibility. This makes individual parts easier to test, replace, and extend.

```
Chess.com API → PGN Parser → Local Storage → Stockfish → Explanation Engine → GUI
```

**Chess.com Sync**
On startup, the app fetches the user's recent games from the Chess.com API and stores them locally for the duration of the session. No repeated API calls are needed while the app is running.

**PGN Parsing**
Games are stored in PGN format (the standard plain-text format used to record chess games). The parser converts this into structured move data that the rest of the application can work with.

**Local Storage**
Parsed games and evaluations are held in local storage during the session. On close, session data is wiped to keep the footprint small. If game data turns out to be negligible in size, persistence across sessions may be added.

**Stockfish Integration**
Each position in a game is evaluated by Stockfish, a leading open-source chess engine. The app communicates with it using UCI (Universal Chess Interface) — a standard protocol for sending positions to a chess engine and receiving evaluations and best moves in return.

**Explanation Engine**
Stockfish produces raw numerical scores. A custom explanation layer interprets these by comparing evaluations move-by-move to detect blunders, mistakes, and strong plays, then producing descriptions of what happened and why.

**GUI**
A desktop interface displays the game list, an interactive board, and a move-by-move analysis panel. The board updates as the user steps through moves, with explanations shown alongside each position.

## PLANNED FEATURES

**Core**
- Sync recent games from Chess.com on startup
- Browse synced games in a game list
- Replay any game move by move on an interactive board
- Visual evaluation bar showing the balance of the position at each move
- Stockfish evaluation score displayed per move
- Automatic detection of standout moves (blunders, mistakes, and great moves)
- Explanations for each standout move
- Session-only local storage (data wiped on close)

**Nice to Have**
- Opening success and failure rates across your game history
- Head-to-head opening matchups (how your openings perform against specific responses)
- External game import — load any PGN directly (Lichess, over-the-board games, or any standard source)
- Persistent storage across sessions

## TECH STACK

- **Language:** C#
- **Framework:** .NET
- **UI:** Avalonia UI (cross-platform)
- **IDE:** Visual Studio Code
- **Chess Engine:** Stockfish (open-source)
- **Engine Communication:** UCI (Universal Chess Interface)
- **Game Data:** Chess.com public API
- **Session Storage:** Local file storage (format TBD)

## ROADMAP

- [x] Chess.com API sync → games appearing in a list
- [ ] PGN parser → moves readable as structured data
- [ ] Session-local storage → games persist during the session
- [ ] Stockfish integration → evaluation scores per move
- [ ] Visual eval bar → position advantage shown graphically
- [ ] Explanation engine → move-by-move descriptions of what happened and why
- [ ] Interactive board → position displayed and replayable
- [ ] Full UI polish → all components tied together cleanly

**Optional**
- [ ] Opening statistics → success/failure rates across game history
- [ ] Head-to-head opening matchups → performance against specific responses
- [ ] External game import → load any PGN directly (Lichess, OTB, or any standard source)
- [ ] Persistent storage across sessions

## STATUS

Pre-build complete. Architecture, features, and tech stack are defined. The GitHub repository is set up with issues tracking each roadmap item. External dependencies (Chess.com API, Stockfish) have been tested and confirmed working. The UI has been sketched. No application code written yet — build starts next.
