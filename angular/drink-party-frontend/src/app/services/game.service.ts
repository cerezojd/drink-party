import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { Player } from '../models/player';
import { StartGameInput } from '../models/start-game-input';
import { StartGameOutput } from '../models/start-game-output';

export const ROOM_CODE_CLAIM_NAME = 'http://bebere.com/claims/RoomCode';
export const PLAYER_NAME_CLAIM_NAME = 'http://bebere.com/claims/PlayerName';
export const PLAYER_ID_CLAIM_NAME = 'sub';
export const LOCAL_STORAGE_TOKEN_KEY = 'token';

@Injectable({ providedIn: 'root' })
export class GameService {
  private currentPlayer = new BehaviorSubject<Player>(null);
  currentPlayer$ = this.currentPlayer.asObservable();
  player: Player;

  constructor(private http: HttpClient) {}

  startGame(playerName: string, roomCode: string): Observable<StartGameOutput> {
    return this.http
      .post<StartGameOutput>('https://localhost:5001/game/startgame', {
        playerName,
        roomCode,
      } as StartGameInput)
      .pipe(
        tap((result) => {
          this.setCurrentSession(result.token, true);
        })
      );
  }

  setCurrentSession(jwt: string, storeToken: boolean = false) {
    const decodedToken = this.decodeJwt(jwt);
    const player: Player = {
      playerName: decodedToken[PLAYER_NAME_CLAIM_NAME],
      roomCode: decodedToken[ROOM_CODE_CLAIM_NAME],
      sessionId: 'teset',
    };

    if (storeToken) {
      this.storeToken(jwt);
    }

    this.setCurrentPlayer(player);
  }

  storeToken(token: string) {
    localStorage.setItem(LOCAL_STORAGE_TOKEN_KEY, token);
  }

  getStoredToken() {
    return localStorage.getItem(LOCAL_STORAGE_TOKEN_KEY);
  }

  getCurrentId(): string {
    return this.decodeJwt(this.getStoredToken())[PLAYER_ID_CLAIM_NAME];
  }

  setCurrentPlayer(player: Player) {
    this.player = player;
    this.currentPlayer.next(player);
  }

  private decodeJwt(jwt: string) {
    return JSON.parse(atob(jwt.split('.')[1]));
  }
}
