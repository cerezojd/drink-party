export interface JoinFormResult {
  playerName: string;
  roomCode: string;
}

export interface MainFormState {
  username?: string;
  roomCode?: string;
}

export enum ScreenMode {
  Join,
  Create,
}
