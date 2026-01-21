import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { DegreeService } from '../../services/degree';
import { Degree } from '../../models/degree.model';

@Component({
  selector: 'app-degrees-list',
  imports: [CommonModule],
  templateUrl: './degrees-list.html',
  styleUrl: './degrees-list.css',
})
export class DegreesList implements OnInit {
  degrees: Degree[] = [];
  loading = false;
  error = '';

  constructor(
    private degreeService: DegreeService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.loadDegrees();
  }

  loadDegrees(): void {
    this.loading = true;
    this.error = '';
    this.degreeService.getAllDegrees().subscribe({
      next: (data) => {
        this.degrees = data;
        this.loading = false;
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.error = 'Error al cargar las carreras';
        this.loading = false;
        console.error(err);
        this.cdr.detectChanges();
      }
    });
  }

  deleteDegree(id: number): void {
    if (confirm('¿Estás seguro de que quieres eliminar esta carrera?')) {
      this.degreeService.deleteDegree(id).subscribe({
        next: () => {
          this.loadDegrees();
        },
        error: (err) => {
          this.error = 'Error al eliminar la carrera';
          console.error(err);
        }
      });
    }
  }

  editDegree(id: number): void {
    this.router.navigate(['/degrees/edit', id]);
  }

  addDegree(): void {
    this.router.navigate(['/degrees/new']);
  }
}
