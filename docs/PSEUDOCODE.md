# Pseudocode

> Pseudocode for the Gambit pipeline, written in the [Cal Poly PDL standard](https://users.csc.calpoly.edu/~jdalbey/SWE/pdl_std.html). Added to as each component is built.

---

## Pipeline

```
Chess.com API → PGN Parser → Local Storage → Stockfish → Explanation Engine → GUI
```

---

## 1. Chess.com API Sync

Fetches the player's recent games from the Chess.com public API on startup, parses the response, and passes each game's PGN to the parser to extract headers and moves.

```
BEGIN
    SET request URL to Chess.com API
    CALL Chess.com API with request URL RETURNING raw json
    PARSE raw json into a document
    GET the games list from the document
    FOR each game in the list
        GET the pgn field
        CALL ParseGame with pgn RETURNING game
        DISPLAY game white player and move count
    ENDFOR
EXCEPTION
    WHEN request fails
        DISPLAY error message
END
```

---

## 2. PGN Parser

Takes a raw PGN string and parses it into a Game object containing metadata (players, ratings, date, result, time control) and a list of moves.

```
ParseHeaders(pgnString)
    SET headers to empty key-value collection
    FOR each line in pgnString
        IF line starts with "["
            SPLIT line on quote character
            SET key to first part with "[" stripped
            SET value to middle part
            STORE key and value in headers
        ENDIF
    ENDFOR
    RETURN headers

ParseMoves(pgnString)
    GET section of pgnString after the blank line
    REMOVE move numbers from section
    SPLIT remaining text on spaces
    RETURN list of moves

ParseGame(pgnString)
    SET game to new Game object
    CALL ParseHeaders with pgnString RETURNING headers
    SET game.White to headers White value
    SET game.Black to headers Black value
    SET game.WhiteElo to headers WhiteElo value
    SET game.BlackElo to headers BlackElo value
    SET game.WonBy to headers Termination value
    SET game.Date to headers Date value
    SET game.TimeControl to headers TimeControl value
    CALL ParseMoves with pgnString RETURNING moves
    SET game.Moves to moves
    RETURN game
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
