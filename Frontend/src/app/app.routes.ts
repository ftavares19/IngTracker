import { Routes } from '@angular/router';
import { DegreesList } from './components/degrees-list/degrees-list';
import { DegreeForm } from './components/degree-form/degree-form';

export const routes: Routes = [
  { path: '', redirectTo: '/degrees', pathMatch: 'full' },
  { path: 'degrees', component: DegreesList },
  { path: 'degrees/new', component: DegreeForm },
  { path: 'degrees/edit/:id', component: DegreeForm }
];
