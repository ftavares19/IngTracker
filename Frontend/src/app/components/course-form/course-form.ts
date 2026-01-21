import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { CourseService } from '../../services/course';
import { DegreeService } from '../../services/degree';
import { AddCourseRequest, ModifyCourseRequest, Semester } from '../../models/course.model';
import { Degree } from '../../models/degree.model';

@Component({
  selector: 'app-course-form',
  imports: [CommonModule, FormsModule],
  templateUrl: './course-form.html',
  styleUrl: './course-form.css',
})
export class CourseForm implements OnInit {
  code = '';
  name = '';
  semester: Semester = Semester.Semester1;
  degreeId: number | null = null;
  
  degrees: Degree[] = [];
  isEditMode = false;
  courseId: number | null = null;
  loading = false;
  error = '';

  semesters = [
    { value: Semester.Semester1, label: 'Semestre 1' },
    { value: Semester.Semester2, label: 'Semestre 2' },
    { value: Semester.Semester3, label: 'Semestre 3' },
    { value: Semester.Semester4, label: 'Semestre 4' },
    { value: Semester.Semester5, label: 'Semestre 5' },
    { value: Semester.Semester6, label: 'Semestre 6' },
    { value: Semester.Semester7, label: 'Semestre 7' },
    { value: Semester.Semester8, label: 'Semestre 8' },
  ];

  constructor(
    private courseService: CourseService,
    private degreeService: DegreeService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.loadDegrees();
    
    this.route.params.subscribe(params => {
      const id = params['id'];
      if (id) {
        this.isEditMode = true;
        this.courseId = +id;
        this.loadCourse(this.courseId);
      }
    });
  }

  loadDegrees(): void {
    this.degreeService.getAllDegrees().subscribe({
      next: (data) => {
        this.degrees = data;
      },
      error: (err) => {
        this.error = 'Error al cargar las carreras';
        console.error(err);
      }
    });
  }

  loadCourse(id: number): void {
    this.loading = true;
    this.courseService.getCourseById(id).subscribe({
      next: (course) => {
        this.code = course.code;
        this.name = course.name;
        this.semester = course.semester;
        this.degreeId = course.degreeId;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Error al cargar la materia';
        this.loading = false;
        console.error(err);
      }
    });
  }

  onSubmit(): void {
    if (!this.code || !this.name || this.degreeId === null) {
      this.error = 'Por favor completa todos los campos requeridos';
      return;
    }

    this.loading = true;
    this.error = '';

    if (this.isEditMode && this.courseId) {
      const request: ModifyCourseRequest = {
        code: this.code,
        name: this.name,
        semester: this.semester,
        degreeId: this.degreeId
      };

      this.courseService.modifyCourse(this.courseId, request).subscribe({
        next: () => {
          this.router.navigate(['/courses']);
        },
        error: (err) => {
          this.error = 'Error al actualizar la materia';
          this.loading = false;
          console.error(err);
        }
      });
    } else {
      const request: AddCourseRequest = {
        code: this.code,
        name: this.name,
        semester: this.semester,
        degreeId: this.degreeId
      };

      this.courseService.addCourse(request).subscribe({
        next: () => {
          this.router.navigate(['/courses']);
        },
        error: (err) => {
          this.error = 'Error al crear la materia';
          this.loading = false;
          console.error(err);
        }
      });
    }
  }

  cancel(): void {
    this.router.navigate(['/courses']);
  }
}
