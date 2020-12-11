import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { SelectGameModePage } from './select-gm.page';

describe('SelectGameModePage', () => {
  let component: SelectGameModePage;
  let fixture: ComponentFixture<SelectGameModePage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SelectGameModePage ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(SelectGameModePage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
