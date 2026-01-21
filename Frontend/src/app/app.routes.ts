import { Routes } from '@angular/router';
import { DegreesList } from './components/degrees-list/degrees-list';
import { DegreeForm } from './components/degree-form/degree-form';
import { CoursesList } from './components/courses-list/courses-list';
import { CourseForm } from './components/course-form/course-form';
import { CoursePrerequisites } from './components/course-prerequisites/course-prerequisites';

export const routes: Routes = [
  { path: '', redirectTo: '/degrees', pathMatch: 'full' },
  { path: 'degrees', component: DegreesList },
  { path: 'degrees/new', component: DegreeForm },
  { path: 'degrees/edit/:id', component: DegreeForm },
  { path: 'courses', component: CoursesList },
  { path: 'courses/new', component: CourseForm },
  { path: 'courses/edit/:id', component: CourseForm },
  { path: 'courses/:id/prerequisites', component: CoursePrerequisites }
];
