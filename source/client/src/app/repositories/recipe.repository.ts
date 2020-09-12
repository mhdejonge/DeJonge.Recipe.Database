import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Recipe } from 'src/app/entites';
import { ApiClient } from './api.client';

@Injectable({
  providedIn: 'root'
})
export class RecipeRepository {
  constructor(private readonly api: ApiClient) { }

  private readonly route = '/api/recipe';

  search(term: string | null | undefined): Observable<Recipe[]> {
    return this.api.get<Recipe[]>(this.route, { term });
  }
}
