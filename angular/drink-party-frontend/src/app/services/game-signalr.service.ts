import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { Observable } from 'rxjs';
import { GameInfo, GameModeType } from '../models/main';
import { PlayerOutput } from '../pages/room/room.component';

@Injectable({ providedIn: 'root' })
export class GameSignalRService {
  private hubConnection: signalR.HubConnection;

  constructor() {}

  configureHub(token: string) {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('/gamehub', {
        accessTokenFactory: () => token,
      })
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this.hubConnection.onclose(() => {
      console.log('Disconnected');
    });
  }

  startConnection(): Promise<void> {
    return this.hubConnection
      .start()
      .then((_) => {
        console.log('Connection started');
      })
      .catch((err) => console.log('Error while starting connection: ', err));
  }

  getPlayers(): Observable<PlayerOutput[]> {
    return this.on('Players');
  }

  getGameInfo(): Observable<GameInfo> {
    return this.on('GameInfo');
  }

  chooseGameMode(gameMode: GameModeType): Observable<void> {
    return this.invoke('ChooseGameMode', gameMode);
  }

  private on<T>(methodName: string): Observable<T> {
    return new Observable<T>((subscriber) => {
      this.hubConnection.on(methodName, (result: T) => subscriber.next(result));
    });
  }

  private invoke<T>(methodName: string, ...args: any[]): Observable<T> {
    return new Observable<T>((subscriber) => {
      this.hubConnection
        .invoke(methodName, ...args)
        .then((result: T) => {
          subscriber.next(result);
        })
        .catch((err) => {
          subscriber.error(err);
        })
        .finally(() => subscriber.complete());
    });
  }
}
