import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { JoinFormResult, MainFormState } from 'src/app/models/main';
import { ScreenMode } from 'src/app/models/screen-mode';
import { GameSignalRService } from 'src/app/services/game-signalr.service';
import { GameStateService } from 'src/app/services/game-state.service';

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
    private gameService: GameSignalRService,
    private gameStateService: GameStateService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.subscribeToGameInfo();
  }

  subscribeToGameInfo() {
    this.gameService.getGameInfo().subscribe((data) => {
      if (!data) {
        return;
      }

      this.gameStateService.saveGameInfo(data);
      this.router.navigate(['room', data.roomCode]);
    });
  }

  onChangeMode(mode: ScreenMode) {
    this.currentMode = mode;
  }

  onCreate(username: string) {
    this.gameService.createRoom(username).subscribe(
      (_) => {
        console.log('Room created successfully');
      },
      (err) => {
        console.error('Room creation error', err);
      }
    );
  }

  onJoin(formResult: JoinFormResult) {}

  onStateChange(formState: MainFormState) {
    this.formState = { ...this.formState, ...formState };
  }
}
