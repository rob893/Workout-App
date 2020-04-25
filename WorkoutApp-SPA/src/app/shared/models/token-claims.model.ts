export interface TokenClaims {
  nameid: string;
  unique_name: string;
  role: string | string[];
  nbf: number;
  exp: number;
  iat: number;
  iss: string;
  aud: string;
}
