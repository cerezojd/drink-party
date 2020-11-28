import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import * as signalR from '@aspnet/signalr';
import { Observable } from 'rxjs';
import { GameInfo } from '../models/game';

const BASE_URL = 'https://localhost:5001';

@Injectable({ providedIn: 'root' })
export class GameSignalRService {
  private hubConnection: signalR.HubConnection;

  constructor(private router: Router) {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(BASE_URL + '/gamehub')
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

  createRoom(userName: string): Observable<void> {
    return this.invoke('CreateRoom', userName);
  }

  getGameInfo(): Observable<GameInfo> {
    return this.on<GameInfo>('gameInfo');
  }

  private on<T>(methodName: string): Observable<T> {
    return new Observable<T>((subscriber) => {
      this.hubConnection.on(methodName, (result: T) => subscriber.next(result));
    });
  }

  private invoke(methodName: string, ...args: any[]): Observable<void> {
    return new Observable<void>((subscriber) => {
      this.hubConnection
        .invoke(methodName, ...args)
        .then((_) => {
          subscriber.next();
        })
        .catch((err) => {
          subscriber.error(err);
        })
        .finally(() => subscriber.complete());
    });
  }
}
