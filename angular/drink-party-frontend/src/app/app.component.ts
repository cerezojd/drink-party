import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { GameSignalRService } from './services/game-signalr.service';

@Component({
  selector: 'app-root',
  template: `<router-outlet></router-outlet>`,
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'drink-party-frontend';

  constructor(
    private gameService: GameSignalRService,
    private http: HttpClient
  ) {}

  generateToken() {
    this.http
      .post<any>('https://localhost:5001/game/generateToken', {
        PlayerName: 'juan',
      })
      .subscribe((result) => {
        localStorage.setItem('jwt', result.token);
        this.connectToSignalR(result.token);
      });
  }

  private connectToSignalR(token: string) {
    this.gameService.configureHub(token);
    this.gameService.startConnection();
  }

  ngOnInit(): void {
    // const existingJwt = localStorage.getItem('jwt');
    // if (existingJwt) {
    //   this.connectToSignalR(existingJwt);
    // } else {
    //   this.generateToken();
    // }
  }
}
