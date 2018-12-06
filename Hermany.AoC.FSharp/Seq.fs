module Seq 

let ring s = 
  seq { while true do yield! s }