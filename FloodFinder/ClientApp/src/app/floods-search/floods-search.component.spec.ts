import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FloodsSearchComponent } from './floods-search.component';

describe('FloodsSearchComponent', () => {
  let component: FloodsSearchComponent;
  let fixture: ComponentFixture<FloodsSearchComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FloodsSearchComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FloodsSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
