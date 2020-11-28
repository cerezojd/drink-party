import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-create-form',
  template: `
    <form [formGroup]="form" (submit)="onSave()">
      <div class="form-group">
        <label>Username</label>
        <input class="form-control" formControlName="username" />
      </div>
      <button class="btn btn-info btn-block" type="submit">
        START THE PARTY!
      </button>
    </form>
  `,
})
export class CreateFormComponent {
  @Output() formResult = new EventEmitter<string>();

  constructor() {}

  form = new FormGroup({
    username: new FormControl('', Validators.required),
  });

  onSave() {
    if (this.form.invalid) {
      return;
    }

    this.formResult.emit(this.form.controls.username.value);
  }
}
