<template>
  <div class="register">
    <h1>Register</h1>
    <p class="error">{{ state.error }}</p>
    <form method="post" @submit.prevent="handleSubmit">
      <input
        type="email"
        v-model="state.email"
        name="email"
        placeholder="Email"
        required
      />
      <input
        type="text"
        v-model="state.username"
        name="username"
        placeholder="Username"
        required
      />
      <input
        type="password"
        v-model="state.password"
        name="password"
        placeholder="Password"
        required
      />
      <input type="submit" value="Send" class="submit-btn" />
    </form>
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive } from "vue";
import Swal from "sweetalert2";
import store from "../../store";
import validate from "./validator";
import { userService } from "../../services";

export default defineComponent({
  name: "Register",
  setup() {
    const state = reactive({
      email: "",
      username: "",
      password: "",
    });

    const handleSubmit = async () => {
      const { email, username, password } = state;
      const isCorrect = validate(state);

      if (!isCorrect) {
        return;
      }

      var message = await userService.register(
        store.state.auth.state.token,
        email,
        username,
        password
      );

      Swal.fire({
        position: "top-end",
        icon: "error",
        title: message,
        showConfirmButton: false,
        timer: 1500,
        width: 300,
      });
    };

    return {
      state,
      handleSubmit,
    };
  },
});
</script>

<styles src="./index.scss"></styles>
