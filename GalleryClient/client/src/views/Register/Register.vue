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
import store from '../../store';
import { userService } from "../../services";

export default defineComponent({
  name: "Register",
  setup() {
    const state = reactive({
      email: "",
      username: "",
      password: "",
      error: ""
    });

    const handleSubmit = async () => {
      const { email, username, password } = state;
      const isCorrect = validate();

      if (!isCorrect) {
        return;
      }
      
      var message = await userService.register(
        store.state.auth.state.token,
        email,
        username,
        password
      );

      state.error = message;
    };

    const validate = () => {
      if (
        state.email.length === 0 ||
        state.username.length === 0 ||
        state.password.length === 0
      ) {
        state.error = "Invalid date!";
        return false;
      }

      if (state.email.length < 4) {
        state.error = "Invalid email!";
        return false;
      }

      if (state.password.length < 6) {
        state.error = "Invalid password!";
        return false;
      }

      return true;
    };

    return {
      state,
      handleSubmit
    };
  }
});
</script>

<styles src="./index.scss"></styles>
