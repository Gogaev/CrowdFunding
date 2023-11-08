import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatedProjectsComponent } from './created-projects.component';

describe('CreatedProjectsComponent', () => {
  let component: CreatedProjectsComponent;
  let fixture: ComponentFixture<CreatedProjectsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CreatedProjectsComponent]
    });
    fixture = TestBed.createComponent(CreatedProjectsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
