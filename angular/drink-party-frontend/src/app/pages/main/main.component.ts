import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { JoinFormResult, MainFormState, ScreenMode } from 'src/app/models/main';
import { GameSignalRService } from 'src/app/services/game-signalr.service';
import { GameService } from 'src/app/services/game.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
})
export class MainComponent implements OnInit {
  screenModes = ScreenMode;
  currentMode = ScreenMode.Create;
  formState: MainFormState;

  constructor(
    private gameSignalRService: GameSignalRService,
    private gameService: GameService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.subscribeToPlayer();
  }

  onChangeMode(mode: ScreenMode) {
    this.currentMode = mode;
  }

  onCreate(playerName: string) {
    this.startGame(playerName);
  }

  onJoin(formResult: JoinFormResult) {
    this.startGame(formResult.playerName, formResult.roomCode);
  }

  private startGame(playerName: string, roomCode?: string) {
    this.gameService.startGame(playerName, roomCode).subscribe(
      (_) => {},
      (_) => {}
    );
  }

  private subscribeToPlayer() {
    this.gameService.currentPlayer$.subscribe((p) => {
      console.log('Player', p);

      if (!p) {
        return;
      }

      this.router.navigate(['/room', p.roomCode]);
    });
  }

  onStateChange(formState: MainFormState) {
    this.formState = { ...this.formState, ...formState };
  }
}
