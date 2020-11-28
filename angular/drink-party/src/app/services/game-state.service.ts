import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { GameInfo } from '../models/game';

@Injectable({ providedIn: 'root' })
export class GameStateService {
  private gameInfo = new BehaviorSubject<GameInfo>(null);
  private gameInfo$ = this.gameInfo.asObservable();

  saveGameInfo(gameInfo: GameInfo) {
    this.gameInfo.next(gameInfo);
  }

  constructor() {}
}
