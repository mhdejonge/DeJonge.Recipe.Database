export class CheckType {
    static boolean(value: any): value is boolean {
        return typeof value === 'boolean';
    }

    static number(value: any): value is number {
        return typeof value === 'number';
    }

    static string(value: any): value is string {
        return typeof value === 'string';
    }
}
