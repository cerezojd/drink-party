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

export enum GameModeType {
  None,
  Coin,
}

export interface GameInfo {
  gameMode: GameModeType;
  started: boolean;
}
