# Pseudocode

> Pseudocode for the Gambit pipeline, written in the [Cal Poly PDL standard](https://users.csc.calpoly.edu/~jdalbey/SWE/pdl_std.html). Added to as each component is built.

---

## Pipeline

```
Chess.com API → PGN Parser → Local Storage → Stockfish → Explanation Engine → GUI
```

---

## 1. Chess.com API Sync

Fetches the player's recent games from the Chess.com public API on startup, parses the response, and extracts the PGN string from each game.

```
BEGIN
    SET request URL to Chess.com API
    CALL Chess.com API with request URL RETURNING raw json
    PARSE raw json into a document
    GET the games list from the document
    FOR each game in the list
        GET the pgn field
        DISPLAY it
    ENDFOR
EXCEPTION
    WHEN request fails
        DISPLAY error message
END
```

---

## 2. PGN Parser

*To be written.*

```

```

---

## 3. Local Storage

*To be written.*

---

## 4. Stockfish Integration

*To be written.*

---

## 5. Explanation Engine

*To be written.*

---

## 6. GUI

*To be written.*
