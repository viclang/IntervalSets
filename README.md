# IntervalRecord
The record Interval<T> will make it easy to deal with https://en.wikipedia.org/wiki/Interval_(mathematics). If you have a validity period you no longer need to figure out the exact business logic.

The interval supports Closed, ClosedOpen, OpenClosed and Open boundaryTypes.
```mermaid
gantt
    Reference :active, a, 2022-01-06, 2022-01-10   

	section Before
    Before :crit, active, b, 2022-01-03, 2022-01-05
   
    section Overlaps when Closed or HalfOpen  
    Meets :crit, active, c, 2022-01-04, 2022-01-06

	section Overlaps
    EndInsideOnly :crit, active, d, 2022-01-05, 2022-01-07
    Starts :crit, active, e, 2022-01-06, 2022-01-08
    ContainedBy :crit, active, f, 2022-01-07, 2022-01-09
    Finishes :crit, active, g, 2022-01-08, 2022-01-10
    Equal :crit, active, h, 2022-01-06, 2022-01-10
    FinishedBy :crit, active, i, 2022-01-05, 2022-01-10
    Contains :crit, active, j, 2022-01-05, 2022-01-11
    StartedBy :crit, active, k, 2022-01-06, 2022-01-11
    StartInsideOnly :crit, active, l, 2022-01-09, 2022-01-11
   
	section Overlaps when Closed or HalfOpen
    MetBy :crit, active, m, 2022-01-10, 2022-01-12

    section After
    After :crit, active, n, 2022-01-11, 2022-01-13
```
