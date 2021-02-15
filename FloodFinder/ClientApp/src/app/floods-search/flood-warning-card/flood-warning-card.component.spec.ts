import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FloodWarningCardComponent } from './flood-warning-card.component';

describe('FloodWarningCardComponent', () => {
  let component: FloodWarningCardComponent;
  let fixture: ComponentFixture<FloodWarningCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FloodWarningCardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FloodWarningCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
