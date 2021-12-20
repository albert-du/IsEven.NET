module IsEven

open System.Threading.Tasks

/// Result from checking parity.
type EvenResult = 
    { Even: bool 
      Ad: string }

/// <summary>
///  Gets if a number if even.
/// </summary>
/// <remarks>
/// Number must be between 0 and 999999 (inclusive).
/// </remarks>
[<CompiledName "IsEven">]
val isEven: number: int -> EvenResult

/// <summary>
///  Gets if a number if even asynchronously.
/// </summary>
/// <remarks>
/// Number must be between 0 and 999999 (inclusive).
/// </remarks>
[<CompiledName "IsEvenAsync">]
val isEvenAsync: number: int -> Task<EvenResult>