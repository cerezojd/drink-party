import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MainFormState } from 'src/app/models/main';

@Component({
  selector: 'app-create-form',
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
      <button class="btn btn-info btn-block" type="submit">
        START THE PARTY!
      </button>
    </form>
  `,
})
export class CreateFormComponent {
  @Input() set state(state: MainFormState) {
    this.fillForm(state?.username);
  }
  @Output() stateChange = new EventEmitter<MainFormState>();
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

  onSaveState() {
    this.stateChange.emit({
      username: this.form.controls.username.value,
    });
  }

  private fillForm(username?: string) {
    this.form.setValue({
      username: username ?? this.form.controls.username.value,
    });
  }
}
