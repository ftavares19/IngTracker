import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DegreeService } from '../../services/degree';
import { Degree } from '../../models/degree.model';

@Component({
  selector: 'app-degree-form',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './degree-form.html',
  styleUrl: './degree-form.css',
})
export class DegreeForm implements OnInit {
  degreeForm: FormGroup;
  isEditMode = false;
  degreeId: number | null = null;
  loading = false;
  error = '';

  constructor(
    private fb: FormBuilder,
    private degreeService: DegreeService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.degreeForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(3)]],
      description: ['', [Validators.required, Validators.minLength(10)]]
    });
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      if (params['id']) {
        this.isEditMode = true;
        this.degreeId = +params['id'];
        this.loadDegree();
      }
    });
  }

  loadDegree(): void {
    if (this.degreeId) {
      this.loading = true;
      this.degreeService.getDegreeById(this.degreeId).subscribe({
        next: (degree) => {
          this.degreeForm.patchValue(degree);
          this.loading = false;
        },
        error: (err) => {
          this.error = 'Error al cargar la carrera';
          this.loading = false;
          console.error(err);
        }
      });
    }
  }

  onSubmit(): void {
    if (this.degreeForm.valid) {
      this.loading = true;
      this.error = '';
      
      const formData = this.degreeForm.value;

      if (this.isEditMode && this.degreeId) {
        this.degreeService.modifyDegree(this.degreeId, formData).subscribe({
          next: () => {
            this.loading = false;
            this.router.navigate(['/degrees']);
          },
          error: (err) => {
            this.error = 'Error al actualizar la carrera';
            this.loading = false;
            console.error(err);
          }
        });
      } else {
        this.degreeService.addDegree(formData).subscribe({
          next: () => {
            this.loading = false;
            this.router.navigate(['/degrees']);
          },
          error: (err) => {
            this.error = 'Error al crear la carrera';
            this.loading = false;
            console.error(err);
          }
        });
      }
    }
  }

  cancel(): void {
    this.router.navigate(['/degrees']);
  }
}
