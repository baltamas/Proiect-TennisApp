import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { Register.PageComponent } from './register.page.component';

describe('Register.PageComponent', () => {
  let component: Register.PageComponent;
  let fixture: ComponentFixture<Register.PageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Register.PageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Register.PageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
