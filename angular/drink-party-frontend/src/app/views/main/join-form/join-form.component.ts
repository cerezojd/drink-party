import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {
  FormGroup,
  FormControl,
  Validators,
  AbstractControl,
} from '@angular/forms';
import { JoinFormResult, MainFormState } from 'src/app/models/main';

@Component({
  selector: 'app-join-form',
  template: `
    <form [formGroup]="form" (submit)="onSave()">
      <div class="form-group">
        <label>Username</label>
        <input
          class="form-control"
          formControlName="username"
          (change)="onSaveState()"
        />
      </div>
      <div class="form-group">
        <label>Room code</label>
        <input
          class="form-control"
          formControlName="roomCode"
          (change)="onSaveState()"
        />
      </div>
      <button class="btn btn-info btn-block" type="submit">
        JOIN TO THE PARTY!
      </button>
    </form>
  `,
})
export class JoinFormComponent {
  @Input() set state(state: MainFormState) {
    this.fillForm(state.username, state.roomCode);
  }
  @Output() stateChange = new EventEmitter<MainFormState>();
  @Output() formResult = new EventEmitter<JoinFormResult>();

  constructor() {}

  form = new FormGroup({
    username: new FormControl('', Validators.required),
    roomCode: new FormControl('', Validators.required),
  });

  get formControls() {
    return this.form.controls;
  }

  onSave() {
    if (this.form.invalid) {
      return;
    }

    this.formResult.emit({
      roomCode: this.formControls.roomCode.value,
      username: this.formControls.username.value,
    } as JoinFormResult);
  }

  onSaveState() {
    this.stateChange.emit({
      roomCode: this.formControls.roomCode.value,
      username: this.formControls.username.value,
    });
  }

  private fillForm(username?: string, roomCode?: string) {
    this.form.setValue({
      roomCode: roomCode ?? this.formControls.roomCode.value,
      username: username ?? this.formControls.username.value,
    });
  }
}
