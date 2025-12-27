export interface Degree {
  id?: number;
  name: string;
  description: string;
}

export interface AddDegreeRequest {
  name: string;
  description: string;
}

export interface ModifyDegreeRequest {
  name: string;
  description: string;
}
