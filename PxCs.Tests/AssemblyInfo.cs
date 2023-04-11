using Xunit;

// Disable parallelization as phoenix values might be dependent on another.
// (And a gothic game will also not run twice in parallel tbh.)
[assembly: CollectionBehavior(DisableTestParallelization = true)]