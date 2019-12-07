// Copyright 2017 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using UnityEngine;

/// <summary>
/// Script that rotates an object at a constant speed.
/// </summary>
public class RotatePlanet : MonoBehaviour {
    public Vector3 axis = new Vector3(5.0f, 1.0f, 0.5f);
    public float angularSpeed = 5.0f;
  private void Update() {
    gameObject.transform.Rotate(axis, angularSpeed * Time.deltaTime, Space.Self);
  }
}

