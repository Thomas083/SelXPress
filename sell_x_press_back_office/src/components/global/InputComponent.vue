<template>
  <label v-if="label!=''" :for="name">{{ label }}</label>
  <input
    :id="id"
    :name="name"
    :type="type"
    :placeholder="placeholder"
    :class="$attrs.class"
    :style="styleObject"
    :value="value"
    @change="$emit('input', parseInt($event.target.value))"
  />
</template>

<script>
export default {
name: "InputComponent",
props: {
  id: String,
  name: String,
  type: String,
  value: [String, Number],
  placeholder: String,
  label: {
    type: String,
    default: "",
  },
  styleObject: Object,
},
};
</script>

<style scoped>
label {
align-self: start;
margin-left: 3vw;
}
</style>
