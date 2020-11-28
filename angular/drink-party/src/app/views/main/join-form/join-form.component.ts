import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { JoinFormResult } from 'src/app/models/join-form-result';

@Component({
  selector: 'app-join-form',
  template: `
    <form [formGroup]="form" (submit)="onSave()">
      <div class="form-group">
        <label>Username</label>
        <input class="form-control" formControlName="username" />
      </div>
      <div class="form-group">
        <label>Room code</label>
        <input class="form-control" formControlName="roomCode" />
      </div>
      <button class="btn btn-info btn-block" type="submit">
        JOIN TO THE PARTY!
      </button>
    </form>
  `,
})
export class JoinFormComponent {
  @Output() formResult = new EventEmitter<JoinFormResult>();

  constructor() {}

  form = new FormGroup({
    username: new FormControl('', Validators.required),
    roomCode: new FormControl('', Validators.required),
  });

  onSave() {
    if (this.form.invalid) {
      return;
    }

    const controls = this.form.controls;
    this.formResult.emit({
      roomCode: controls.roomCode.value,
      username: controls.username.value,
    } as JoinFormResult);
  }
}
