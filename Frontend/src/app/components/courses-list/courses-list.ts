import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { CourseService } from '../../services/course';
import { DegreeService } from '../../services/degree';
import { Course } from '../../models/course.model';
import { Degree } from '../../models/degree.model';

@Component({
  selector: 'app-courses-list',
  imports: [CommonModule],
  templateUrl: './courses-list.html',
  styleUrl: './courses-list.css',
})
export class CoursesList implements OnInit {
  courses: Course[] = [];
  degrees: Degree[] = [];
  loading = false;
  error = '';
  selectedDegree: number | null = null;

  constructor(
    private courseService: CourseService,
    private degreeService: DegreeService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.loadDegrees();
    this.loadCourses();
  }

  loadDegrees(): void {
    this.degreeService.getAllDegrees().subscribe({
      next: (data) => {
        this.degrees = data;
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error('Error loading degrees:', err);
      }
    });
  }

  loadCourses(): void {
    this.loading = true;
    this.error = '';
    this.courseService.getAllCourses().subscribe({
      next: (data) => {
        this.courses = data;
        this.loading = false;
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.error = 'Error al cargar las materias';
        this.loading = false;
        console.error(err);
        this.cdr.detectChanges();
      }
    });
  }

  get filteredCourses(): Course[] {
    if (!this.selectedDegree) return this.courses;
    return this.courses.filter(c => c.degreeId === this.selectedDegree);
  }

  getSemesterName(semester: number): string {
    return `Semestre ${semester + 1}`;
  }

  getDegreeName(degreeId: number): string {
    const degree = this.degrees.find(d => d.id === degreeId);
    return degree ? degree.name : 'N/A';
  }

  deleteCourse(id: number): void {
    if (confirm('¿Estás seguro de que quieres eliminar esta materia?')) {
      this.courseService.deleteCourse(id).subscribe({
        next: () => {
          this.loadCourses();
        },
        error: (err) => {
          this.error = 'Error al eliminar la materia';
          console.error(err);
        }
      });
    }
  }

  editCourse(id: number): void {
    this.router.navigate(['/courses/edit', id]);
  }

  addCourse(): void {
    this.router.navigate(['/courses/new']);
  }

  filterByDegree(degreeId: number | null): void {
    this.selectedDegree = degreeId;
  }

  managePrerequisites(id: number): void {
    this.router.navigate(['/courses', id, 'prerequisites']);
  }
}
